using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace izBackend.Models.Common
{
    public class APIResponse<T> : ActionResult
    {
        
        public bool Success { get; set; }
        public string message = "";
        public T Data { get; set; }
        public int? TotalCount;
        public Exception exception;
        public String error = "";
       
        public APIResponse()
        {
           
        }

        public APIResponse(String error, Exception e)
        {

            this.Success = false;
            this.exception = e;
            this.error = error;

        }

        public APIResponse(T data)
        {
            this.Success = true;
            this.Data = data;
            this.TotalCount = GetCount(data);
        }

        public int? GetCount(object t)
        {
            int? count = null;
            if (t is IEnumerable)
            {
                count = 0;
                foreach (object item in (IEnumerable)t)
                {
                    count++;
                }
            }
            return count;
        }

      
    }

    public class APIResponseSuccess : APIResponse<String>
    {
        public APIResponseSuccess(String message)
        {
            this.Success = true;
            this.message = message;
        }
        public APIResponseSuccess()
        {
            this.Success = true;
            this.Data ="";
        }
    }
    public class APIResponseError : APIResponse<string>
    {
        public APIResponseError()
        {
            this.Success = false;
            this.Data = "";
        }
        public APIResponseError(string message)
        {
            this.Success = false;
            this.message = message;
            this.Data = "";
        }
    }
}
