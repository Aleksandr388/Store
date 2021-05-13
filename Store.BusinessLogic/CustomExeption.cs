using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;

namespace Store.BusinessLogic
{
    public class CustomExeption : Exception
    {
        
        public string Error { get; set; }
        public int StatusCode { get; set; }

        public CustomExeption()
        {
        }

        public CustomExeption(int statusCode, string error)
        {
            Error = error;
            StatusCode = statusCode;
        }
    }
}

