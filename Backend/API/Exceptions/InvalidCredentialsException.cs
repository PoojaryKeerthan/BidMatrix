using Microsoft.AspNetCore.Http;

namespace API.Exceptions
{
    public class InvalidCredentialsException : AppException
    {
        public InvalidCredentialsException(string message) : base(message, StatusCodes.Status401Unauthorized)
        {
        }
    }
}
