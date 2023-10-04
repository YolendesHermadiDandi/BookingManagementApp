using System.Net;

namespace API.Utilities.Handler
{
    public class ResponseDataNotFoundHandler
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string? Error { get; set; }


        public ResponseDataNotFoundHandler(string message)
        {
            Code = StatusCodes.Status404NotFound;
            Status = HttpStatusCode.NotFound.ToString();
            Message = message;
        }

        public ResponseDataNotFoundHandler(string message, ExceptionHandler ex)
        {
            Code = StatusCodes.Status404NotFound;
            Status = HttpStatusCode.NotFound.ToString();
            Message = message;
            Error = ex.Message;
        }

    }
}
