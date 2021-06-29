using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSample.Auth.Models
{
    public class TokenModel
    {
        public string JwtToken { get; set; }

        public Guid RefreshToken { get; set; }
    }
}
