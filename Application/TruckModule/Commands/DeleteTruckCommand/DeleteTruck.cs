using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.TruckModule.Commands.DeleteTruckCommand;

public record DeleteTruck: IRequest<CustomResponse> {
    public int Id {get; set;}
}

public class DeleteTruckHandler: IRequestHandler<DeleteTruck, CustomResponse> {

    private readonly IAppDbContext _context;

    public DeleteTruckHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<CustomResponse> Handle(DeleteTruck request, CancellationToken cancellationToken){
        
        var found_truck = await _context.Trucks.FindAsync(request.Id);
        if(found_truck == null){
            throw new GhionException(CustomResponse.NotFound($"Truck with id = {request.Id} is not found"));
        }
        _context.Trucks.Remove(found_truck);
            await _context.SaveChangesAsync(cancellationToken);

         return CustomResponse.Succeeded("Truck deleted successfully!");

    }
}