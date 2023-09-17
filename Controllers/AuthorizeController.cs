using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ASPAPI.Dtos;
using ASPAPI.Services.RefreshTokenService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ASPAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenHandler _refresh;
        public AuthorizeController(DataContext dataContext, IOptions<JwtSettings> options,
        IRefreshTokenHandler refreshTokenHandler ){
            _dataContext = dataContext;
            _jwtSettings = options.Value;
            _refresh = refreshTokenHandler;
        }
        
       
        [HttpPost("GenerateToken")]
        public async Task<IActionResult> GenerateToken([FromBody] UserCredential userCred){
            var user = await _dataContext.Users.FirstOrDefaultAsync(item => item.Code == userCred.UserName 
                && item.Password == userCred.Password);
            if (user is not null) {
                //generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey);
                var tokenDesc = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, user.Code),                     
                        new Claim(ClaimTypes.Role, user.Role)
                    }),
                    Expires = DateTime.UtcNow.AddSeconds(3600),
                    SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(tokenKey), 
                    SecurityAlgorithms.HmacSha256)
                };
                var token =tokenHandler.CreateToken(tokenDesc);
                var finalToken = tokenHandler.WriteToken(token);
                return Ok(new TokenResponse(){Token=finalToken, RefreshToken=await _refresh.GenerateToken(user.Name)});
             }else{
                return Unauthorized();
            }
        }
        
        [HttpPost("GenerateRefreshToken")]
        public async Task<IActionResult> GenerateToken([FromBody] TokenResponse token)
        {
            //check the refresh token existence
            var refreshToken = await _dataContext.TblRefreshToken.FirstOrDefaultAsync(item => item.RefreshToken == token.RefreshToken);
            if(refreshToken != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey);
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token.Token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
                    ValidateIssuer = false,
                    ValidateAudience=false,
                }, out securityToken);

                var _token = securityToken as JwtSecurityToken;
                if(_token != null && _token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                {
                    string userName = principal.Identity?.Name;
                    var _existData = await _dataContext.TblRefreshToken.FirstOrDefaultAsync(item => item.UserId == userName
                    && item.RefreshToken == token.RefreshToken);
                    if(_existData is null){
                        return Unauthorized();
                    }
                    else
                    {
                        var newToken = new JwtSecurityToken(
                            claims: principal.Claims.ToArray(),
                            expires: DateTime.Now.AddSeconds(3600),
                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey)
                            ), SecurityAlgorithms.HmacSha256)
                        );
                        var finalToken = tokenHandler.WriteToken(newToken);
                        return Ok(new TokenResponse(){Token=finalToken, RefreshToken=await _refresh.GenerateToken(userName)});

                    }
                }else{
                    return Unauthorized();
                }

            }else{
                return Unauthorized();
            }
        }

      
    }
}