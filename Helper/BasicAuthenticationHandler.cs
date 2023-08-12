using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace ASPAPI.Helper
{
    public class BasicAuthenticationHandler:AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly DataContext _context;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
         ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, DataContext context) : base(options, logger, encoder, clock)
        {
            _context = context;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //HandleAuthenticateAsync、HandleChallengeAsync、HandleSignInAsync、HandleSignOutAsync 
            if (!Request.Headers.ContainsKey("Authorization")){
                return AuthenticateResult.Fail("Not header found");
            }
            // In controller classes, we can use Request,Response,User,ModelState, Url etc.
            var headerValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            if( headerValue is not null){
                var bytes = Convert.FromBase64String(headerValue.Parameter);
                string credentials = Encoding.UTF8.GetString(bytes);
                string[] array = credentials.Split(":");
                string username = array[0];
                string password = array[1];
                var user = await _context.Users.FirstOrDefaultAsync(item => item.Code == username 
                && item.Password == password);
                if(user is not null){
                    var claim = new[] { new Claim(ClaimTypes.Name, user.Code) };
                    var identity = new ClaimsIdentity(claim, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                }else{
                    return AuthenticateResult.Fail("UnAuthorized");
                }
            }else{
                return AuthenticateResult.Fail("Empty header");
            }
        }
    }
}