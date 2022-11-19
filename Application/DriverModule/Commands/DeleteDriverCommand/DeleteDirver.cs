using System.ComponentModel;
using Application.CompanyModule.Queries;
using Domain.Entities;
using MediatR;
using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.DriverModule.Commands.DeleteDriverCommand;

public record DeleteDriver : IRequest<CustomResponse> {
    public int Id {get; set;}
}

public class DeleteDriverHandler : IRequestHandler<DeleteDriver, CustomResponse> {

    private readonly IAppDbContext _context;

    public DeleteDriverHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
    }

    public async Task<CustomResponse> Handle(DeleteDriver request, CancellationToken cancellationToken){

        var driver = await _context.Drivers.FindAsync(request.Id);

        if(driver != null){
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync(cancellationToken);
        }

        throw new Exception("lsjdfl");
        return CustomResponse.Succeeded("Driver deleted Successfully");

    }

}