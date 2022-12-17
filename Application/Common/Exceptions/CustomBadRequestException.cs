
namespace Application.Common.Exceptions
{
    public class CustomBadRequestException : Exception
    {
        public CustomBadRequestException() : base("bad request")
        {

        }
        public CustomBadRequestException(string message) : base(message)
        {

        }
    }
}
