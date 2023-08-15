using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPAPI.Dtos
{
    public class JwtSettings
    {
        public string SecurityKey { get; set; } = null!;
    }
}