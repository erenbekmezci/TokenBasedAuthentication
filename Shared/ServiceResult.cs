using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ServiceResult<T> where T : class
    {
        public T? Data { get; set; }
        public List<string>? ErrorMessage { get; set; }

        public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
        public bool IsFail => !IsSuccess;
        public HttpStatusCode Status { get; set; }
        public string? UrlCreated { get; set; }
        public static ServiceResult<T> Success(T data,HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ServiceResult<T> 
            {
                Data = data,
                Status = statusCode,
            };

        }
        public static ServiceResult<T> SuccessCreated(T data,string urlCreated)
        {
            return new ServiceResult<T>
            {
                UrlCreated = urlCreated,
                Data = data,
                Status = HttpStatusCode.Created,
            };

        }
        public static ServiceResult<T> Fail(string errorMessage , HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                //data null
                ErrorMessage = new List<string>() { errorMessage },  
                Status = statusCode,
            };

        }
        public static ServiceResult<T> Fail(List<string> errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                //data null
                ErrorMessage = errorMessage,
                Status = statusCode,
            };

        }


    }
    public class ServiceResult
    {
        public List<string>? ErrorMessage { get; set; }

        public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
        public bool IsFail => !IsSuccess;
        public HttpStatusCode Status { get; set; }

        public static ServiceResult Success(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ServiceResult
            {
                
                Status = statusCode,
            };

        }
        public static ServiceResult Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                ErrorMessage = errorMessage,
                Status = status,
            };
        }

        public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                ErrorMessage = new List<string>() { errorMessage }, //  c# 8 [errorMessage]
                Status = status,
            };
        }


    }
}
