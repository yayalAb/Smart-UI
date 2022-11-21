using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.LookUp.Commands.CreateLookUpKey;

public record CreateLookUpKey : IRequest<CustomResponse> {
    public string Key { get; init; }

    public CreateLookUpKey(string name){
        this.Key = name;
    }
}

public class CreateLookUpKeyHandler : IRequestHandler<CreateLookUpKey, CustomResponse> {
    private readonly IAppDbContext _context;

    public CreateLookUpKeyHandler(IAppDbContext context) {
        _context = context;
    }
    public async Task<CustomResponse> Handle(CreateLookUpKey request, CancellationToken cancellationToken) {

        Lookup newLookup = new Lookup() {
            Key = "key",
            Value = request.Key,
            IsParent = 1
        };

        await _context.Lookups.AddAsync(newLookup);
        await _context.SaveChangesAsync(cancellationToken);
        return CustomResponse.Succeeded("Lookup category created!");
        
    }
}