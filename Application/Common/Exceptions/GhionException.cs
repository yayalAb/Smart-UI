using Application.Common.Models;

namespace Application.Common.Exceptions;

public class GhionException : Exception
{

    public CustomResponse Response { get; set; } = null!;

    public GhionException(CustomResponse response) : base(response.Message)
    {
        this.Response = response;
    }

}