using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(T data, bool success, string message):this(data, success)
        {
            Message = message;
        }
        public DataResult(T data, bool success)
        {
            Data = data;
            Success = success;
        }
        public T Data { get; }

        public bool Success { get; }

        public string Message { get; }
    }
}
