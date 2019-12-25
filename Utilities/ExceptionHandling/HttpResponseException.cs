using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Utilities.ExceptionHandling
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException(string message): base(message)
        {
        }

        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
}
