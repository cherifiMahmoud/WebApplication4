using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication4.Core.ApiModes
{
    public class CheckEmailApiModel
    {
        public class Request
        {
            //Email to check 
            [EmailAddress]
            public string Email { get; set; }
        }

        public class Response 
        { 
            public bool Exists { get; set; }
        }
    }
}
