using MediatR;
using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.CompanyModule.Commands.DeleteCompanyCommand;

public record DeleteCompany : IRequest<CustomResponse> {
    public int Id {get; set;}
}

public class DeleteCompanyHandler : IRequestHandler<DeleteCompany, CustomResponse> {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public DeleteCompanyHandler(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomResponse> Handle(DeleteCompany request, CancellationToken cancellationToken){
        
        var found_Company = await _context.Companies
                .Include(c => c.Address)
                .Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync();
        if(found_Company == null){
            throw new GhionException(CustomResponse.NotFound($"Company with id = {request.Id} is not found"));
        }
        var address = found_Company.Address;
      //TODO: address relationship on delete
        _context.BankInformation.RemoveRange(found_Company.BankInformation);
        _context.Companies.Remove(found_Company);
        await _context.SaveChangesAsync(cancellationToken);
        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync(cancellationToken);
         return CustomResponse.Succeeded("Company deleted successfully!");
    }

}