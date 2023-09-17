using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace ASPAPI.Services.RefreshTokenService
{
    public class RefreshTokenHandler : IRefreshTokenHandler
    {

         private readonly DataContext _context;
        public RefreshTokenHandler(DataContext context){
            _context = context;
        }
        public async Task<string> GenerateToken(string username)
        {
            var randomNumber = new byte[32];
            using(var randomNumberGenerator= RandomNumberGenerator.Create()){
                randomNumberGenerator.GetBytes(randomNumber);
                string refreshToken = Convert.ToBase64String(randomNumber);
                var ExistToken = await _context.TblRefreshToken.FirstOrDefaultAsync(item => item.UserId == username);
                if(ExistToken is not null){
                    ExistToken.RefreshToken = refreshToken;
                }else{
                    await _context.TblRefreshToken.AddAsync(new TblRefreshToken
                    {
                        UserId = username,
                        //Random().Next(1,10001), the default range is  1-99(Next(1,100));
                        TokenId = new Random().Next().ToString(),
                        RefreshToken = refreshToken
                    });
                }
                await _context.SaveChangesAsync();
                return refreshToken;
            } 
        }


    }
}