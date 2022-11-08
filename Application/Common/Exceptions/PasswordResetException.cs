

namespace Application.Common.Exceptions
{
    public class PasswordResetException : Exception
    {
        public PasswordResetException() : base("password reset failed")
        {

        }
        public PasswordResetException(string message) : base("password reset failed\n" + message)
        {

        }


    }
}
