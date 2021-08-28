using System;
using System.Collections.Generic;
using System.Net;

namespace Store.BusinessLogic
{
    public class CustomException : Exception
    {
        public CustomException()
        {
        }

        public List<string> Error { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public CustomException(string error, HttpStatusCode statusCode)
        {
            Error = new List<string>() { error };
            StatusCode = statusCode;
        }

        public CustomException(HttpStatusCode statusCode, List<string> error)
        {
            Error = error;
            StatusCode = statusCode;
        }
    }
}

