using Application.CompanyModule.Queries;
using Domain.Entities;
using MediatR;
using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.ContainerModule.Commands.ContainerDelete;

public record ContainerDelete : IRequest<CustomResponse> {
    public int Id {get; init;}
}

public class ContainerDeleteHandler : IRequestHandler<ContainerDelete, CustomResponse> {
    private readonly IAppDbContext _context;

    public ContainerDeleteHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
    }

    public async Task<CustomResponse> Handle(ContainerDelete request, CancellationToken cancellationToken){
        
        var container = await _context.Containers.FindAsync(request.Id);

        if(container != null) {
            _context.Containers.Remove(container);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return CustomResponse.Succeeded("Container Deleted Successfully!");

    }
}