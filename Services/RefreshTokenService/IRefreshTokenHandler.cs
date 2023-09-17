using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPAPI.Services.RefreshTokenService
{
    public interface IRefreshTokenHandler
    {
        Task<string> GenerateToken(string username);
    }
}