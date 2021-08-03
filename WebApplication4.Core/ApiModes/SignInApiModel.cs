using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication4.Core.ApiModes
{
    public class SignInApiModel
    {
        public class RequestSignIn
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
        }

        public class ResponseSingIn
        {
            public string Token { get; set; }
        }
    }
}
