
namespace Application.Common.Exceptions
{
    public class CustomBadRequestException : Exception
    {
        public CustomBadRequestException():base()
        {
            Errors = new List<string>();
        }
        public CustomBadRequestException(string message) : base(message)
        {

        }
        public CustomBadRequestException(IEnumerable<string> errors):this()
        {
            Errors = errors;
        }
        public IEnumerable<string>? Errors{ get; }
    }
}
