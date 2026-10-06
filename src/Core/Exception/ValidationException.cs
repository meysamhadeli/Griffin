using System.Net;

namespace Griffin.Core.Exception
{
    public class ValidationException : CustomException
    {
        public ValidationException(string message, int? code = null) : base(message, HttpStatusCode.BadRequest, code: code)
        {
        }
    }
}