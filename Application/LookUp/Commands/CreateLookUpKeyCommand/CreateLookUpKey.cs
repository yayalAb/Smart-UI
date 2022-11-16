using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.LookUp.Commands.CreateLookUpKey;

public record CreateLookUpKey : IRequest<int> {
    public string Key { get; init; }

    public CreateLookUpKey(string name){
        this.Key = name;
    }
}

public class CreateLookUpKeyHandler : IRequestHandler<CreateLookUpKey, int> {
    private readonly IAppDbContext _context;

    public CreateLookUpKeyHandler(IAppDbContext context) {
        _context = context;
    }
    public async Task<int> Handle(CreateLookUpKey request, CancellationToken cancellationToken) {

        Lookup newLookup = new Lookup
        {
            Value = request.Key,
            Key = "key",
        };

        await _context.Lookups.AddAsync(newLookup);
        await _context.SaveChangesAsync(cancellationToken);
        return newLookup.Id;
        
    }
}