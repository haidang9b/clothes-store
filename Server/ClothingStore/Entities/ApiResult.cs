﻿namespace ClothingStore.Entities
{
    public class ApiResult
    {
        public int HttpStatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public ApiResult(object data = null, bool isSuccess = true, string message = "", int httpStatusCode = 200)
        {
            Data = data;
            HttpStatusCode = httpStatusCode;
            IsSuccess = isSuccess;
            Message = message;
        }

        public void InternalError()
        {
            HttpStatusCode = 500;
            Message = "Internal Server Error";
            IsSuccess = false;
        }
    }
}
