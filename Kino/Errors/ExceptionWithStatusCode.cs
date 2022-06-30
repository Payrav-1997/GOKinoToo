using System.Net;

namespace Kino.Errors;

public class ExceptionWithStatusCode : Exception
{
    public ExceptionWithStatusCode(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; set; }
}

