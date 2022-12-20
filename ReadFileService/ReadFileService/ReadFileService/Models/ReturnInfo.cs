using Microsoft.AspNetCore.Mvc;

namespace ReadFileService.Models
{
    public class ReturnInfo<T>
    {
        public ReturnInfo(T content)
        { Content = content; }

        public ReturnInfo(HttpError error)
        {
            ExceptionError = error;
        }

        public T Content { get; set; }
        public HttpError? ExceptionError { get; set; }

        public bool IsException => ExceptionError != null;

        public ActionResult<T> ToContent()
    => !IsException ? ToVariable : new ContentResult
    {
        Content = ExceptionError.Message,
        StatusCode = ExceptionError.Code,
    };

        private ActionResult<T> ToVariable
            => Content is null ? new ContentResult()
            {
                StatusCode = 200
            } : Content;
    }

    public class HttpError
    {
        public HttpError(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; set; }
        public string Message { get; set; }
    }
}