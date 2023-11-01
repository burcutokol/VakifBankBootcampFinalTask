using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaProject
{
    public class TokenRequest
    {
        public string Email { get; set; }   
        public string Password { get; set; }
    }
    public class TokenResponse
    {
        public DateTime ExpireDate { get; set; }
        public string Token { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
