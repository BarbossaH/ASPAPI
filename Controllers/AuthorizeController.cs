using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ASPAPI.Dtos;
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
        public AuthorizeController(DataContext dataContext, IOptions<JwtSettings> options ){
            _dataContext = dataContext;
            _jwtSettings = options.Value;
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
                return Ok(finalToken);
             }else{
                return Unauthorized();
            }
        }
    }
}