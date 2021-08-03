using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication4.Core.ApiModes
{
    public class ApiResponse
    {
        public bool Success => Errors ==null ;

        public List<int> Errors { get; private set; }

        public void AddError(int error)
        {
            if (Errors == null)
                Errors = new List<int>();

            Errors.Add(error);
        }

        public void AddErrors(List<int> errors)
        {
            if (Errors == null)
                Errors = new List<int>();

            Errors.AddRange(errors);
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data {get; set; }
    }
}
