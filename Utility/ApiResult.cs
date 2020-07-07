using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExcel.Utility
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public ApiResult(bool isSuccess, object data = null, string message = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
    }
}
