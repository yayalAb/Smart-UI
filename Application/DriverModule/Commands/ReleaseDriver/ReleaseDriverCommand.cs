using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.DriverModule.Commands.ReleaseDriver;
public record ReleaseDriverCommand : IRequest<CustomResponse>
{
    public int Id { get; set; }
}
public class ReleaseDriverCommandHandler : IRequestHandler<ReleaseDriverCommand, CustomResponse>
{
    private readonly IAppDbContext _context;

    public ReleaseDriverCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<CustomResponse> Handle(ReleaseDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers.FindAsync(request.Id);
        if (driver == null)
        {
            throw new GhionException(CustomResponse.NotFound($"driver with id = {request.Id} is not found"));

        }
        if (!driver.IsAssigned)
        {
            return CustomResponse.Succeeded("driver is already unassigned");
        }
        driver.IsAssigned = false;
        _context.Drivers.Update(driver);
        await _context.SaveChangesAsync(cancellationToken);
        return CustomResponse.Succeeded("driver released  successfully!");
    }
}
