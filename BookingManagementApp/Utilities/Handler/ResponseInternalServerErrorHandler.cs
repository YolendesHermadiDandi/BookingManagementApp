using System.Net;

namespace API.Utilities.Handler
{
    public class ResponseInternalServerErrorHandler
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string? Error { get; set; }

        public ResponseInternalServerErrorHandler(string message)
        {
            Code = StatusCodes.Status500InternalServerError;
            Status = HttpStatusCode.NotFound.ToString();
            Message = message;
        }

        public ResponseInternalServerErrorHandler(string message, ExceptionHandler ex)
        {
            Code = StatusCodes.Status500InternalServerError;
            Status = HttpStatusCode.NotFound.ToString();
            Message = message;
            Error = ex.Message;
        }


    }
}
