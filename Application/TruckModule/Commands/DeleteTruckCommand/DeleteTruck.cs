using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.TruckModule.Commands.DeleteTruckCommand;

public record DeleteTruck: IRequest<string> {
    public int Id {get; set;}
}

public class DeleteTruckHandler: IRequestHandler<DeleteTruck, string> {

    private readonly IAppDbContext _context;

    public DeleteTruckHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(DeleteTruck request, CancellationToken cancellationToken){
        
        var found_truck = await _context.Trucks.FindAsync(request.Id);
        if(found_truck != null){
            _context.Trucks.Remove(found_truck);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return "Truck removed successfully";

    }
}