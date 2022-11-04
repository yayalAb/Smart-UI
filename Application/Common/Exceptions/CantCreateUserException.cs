
namespace Application.Common.Exceptions
{
    public class CantCreateUserException : Exception
    {
        public CantCreateUserException() : base("could not create user")
        {
            Errors = new List<string>();
        }
        public CantCreateUserException(List<string> errors):this()
        {
            Errors = errors;    

        }
        public IEnumerable<string> Errors { get; }
    }
}
