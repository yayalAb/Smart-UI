using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.PortModule.Commands.DeletePort;

public class DeletePort : IRequest<string> {
    public int Id {get; set;}
}

public class DeletePortHandler: IRequestHandler<DeletePort, string> {

    private readonly IAppDbContext _context;

    public DeletePortHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(DeletePort request, CancellationToken cancellationToken) {
        
        var found_port = await _context.Ports.FindAsync(request.Id);
        if(found_port != null){
            _context.Ports.Remove(found_port);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        return "Port deleted successfully!";

    }

}