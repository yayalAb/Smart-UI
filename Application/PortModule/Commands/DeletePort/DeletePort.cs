using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.PortModule.Commands.DeletePort;

public class DeletePort : IRequest<CustomResponse> {
    public int Id {get; set;}
}

public class DeletePortHandler: IRequestHandler<DeletePort, CustomResponse> {

    private readonly IAppDbContext _context;

    public DeletePortHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<CustomResponse> Handle(DeletePort request, CancellationToken cancellationToken) {
        
        var found_Port = await _context.Ports.FindAsync(request.Id);
        if(found_Port == null){
            throw new GhionException(CustomResponse.NotFound($"Port with id = {request.Id} is not found"));
        }
        _context.Ports.Remove(found_Port);
            await _context.SaveChangesAsync(cancellationToken);

         return CustomResponse.Succeeded("Port deleted successfully!");

}
}