
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.OperationDocuments.SNumberUpdate;

public record SetSNumber : IRequest<CustomResponse>
{
    public string SNumber { get; init; }
    public int OperationId { get; init; }
}

public class SetSNumberHandler : IRequestHandler<SetSNumber, CustomResponse>
{

    private readonly IAppDbContext _context;

    public SetSNumberHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<CustomResponse> Handle(SetSNumber request, CancellationToken cancellationToken)
    {
        var operation = await _context.Operations.FindAsync(request.OperationId);
        if (operation == null)
        {
            throw new GhionException(CustomResponse.NotFound("Operation not found!"));
        }

        operation.SNumber = request.SNumber;
        operation.SDate = DateTime.Now;

        _context.Operations.Update(operation);
        await _context.SaveChangesAsync(cancellationToken);

        return CustomResponse.Succeeded("Snumber updated");

    }
}

