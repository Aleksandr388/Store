using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;

namespace Store.BusinessLogic
{
    public class CustomException : Exception
    {
        
        public List<string> Error { get; set; }
        public int StatusCode { get; set; }

        public CustomException(string error, int statusCode)
        {
            Error = new List<string>() { error };
            StatusCode = statusCode;
        }

        public CustomException(int statusCode, List<string> error)
        {
            Error = error;
            StatusCode = statusCode;
        }
    }
}

