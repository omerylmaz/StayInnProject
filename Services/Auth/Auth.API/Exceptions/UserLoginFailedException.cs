using BuildingBlocks.Exceptions;

namespace Auth.API.Exceptions
{
    [Serializable]
    internal class UserLoginFailedException : BadRequestException
    {

        public UserLoginFailedException(string? message) : base(message)
        {
        }

        public UserLoginFailedException(string message, string details) : base(message)
        {
        }
    }
}