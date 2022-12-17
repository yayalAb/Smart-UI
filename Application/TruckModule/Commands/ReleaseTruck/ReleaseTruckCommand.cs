using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.TruckModule.Commands.ReleaseTruck;
public record ReleaseTruckCommand : IRequest<CustomResponse>
{
    public int Id { get; set; }
}
public class ReleaseTruckCommandHandler : IRequestHandler<ReleaseTruckCommand, CustomResponse>
{
    private readonly IAppDbContext _context;

    public ReleaseTruckCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<CustomResponse> Handle(ReleaseTruckCommand request, CancellationToken cancellationToken)
    {
        var Truck = await _context.Trucks.FindAsync(request.Id);
        if (Truck == null)
        {
            throw new GhionException(CustomResponse.NotFound($"Truck with id = {request.Id} is not found"));

        }
        if (!Truck.IsAssigned)
        {
            return CustomResponse.Succeeded("Truck is already unassigned");
        }
        Truck.IsAssigned = false;
        _context.Trucks.Update(Truck);
        await _context.SaveChangesAsync(cancellationToken);
        return CustomResponse.Succeeded("Truck released  successfully!");
    }
}
