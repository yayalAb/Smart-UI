
namespace Application.Common.Exceptions;

public class CannotUpdateUserException : Exception {

    public CannotUpdateUserException() : base("could not create user") {
        Errors = new List<string>();
    }
    public CannotUpdateUserException(List<string> errors):this() {
        Errors = errors;

    }
    public IEnumerable<string> Errors { get; }

}