using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaProject
{
    public class UserRequest
    {
        public int UserLoginId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string? Role { get; set; }
        public DateTime LastActivityDate { get; set; }
        public int PasswordRetryCount { get; set; }
        public int Status { get; set; }
    }
    public class UserResponse
    {
        public int UserLoginId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }

    }
}
