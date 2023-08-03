using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPAPI.Models
{
    public class ServiceResponse<T>
    {
    public T ? Data{ set; get; }

    public int ResponseCode { get; set; } = 200;
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
  }
}