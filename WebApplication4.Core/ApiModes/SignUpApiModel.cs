using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication4.Core.ApiModes
{
    public class SignUpApiModel
    {
        public class RequestSignUp
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [MinLength(8)]
            [MaxLength(20)]
            public string Password { get; set; }
        }

        public class ResponseSingUp
        {
            public string Email { get; set; }
        }
    }
}
