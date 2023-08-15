using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPAPI.Dtos
{
    public class UserCredential
    {
        public string UserName { get; set; } = null!;
        public string Password{ get; set; }= null!;
    }
}