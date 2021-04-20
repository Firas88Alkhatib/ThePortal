using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThePortal.Models.LoginAndRegister
{
    public class ExchangeTokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
