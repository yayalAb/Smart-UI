
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverModule.Commands.DeleteDriverCommand;

public record DeleteDriver : IRequest<CustomResponse>
{
    public int Id { get; set; }
}

public class DeleteDriverHandler : IRequestHandler<DeleteDriver, CustomResponse>
{

    private readonly IAppDbContext _context;

    public DeleteDriverHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
    }

    public async Task<CustomResponse> Handle(DeleteDriver request, CancellationToken cancellationToken)
    {

        var driver = await _context.Drivers
                        .Include(d => d.Address)
                        .Where(d => d.Id == request.Id)
                        .FirstOrDefaultAsync();
       

        if (driver != null)
        {
             var driverAddress = driver.Address;
            _context.Drivers.Remove(driver);
            _context.Addresses.Remove(driverAddress);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Driver deleted successfully!");
        }

        throw new Common.Exceptions.GhionException(CustomResponse.NotFound($"Driver with id = {request.Id} is not found"));

    }

}