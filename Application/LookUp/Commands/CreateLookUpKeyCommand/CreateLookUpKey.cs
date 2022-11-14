using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.LookUp.Commands.CreateLookUpKey;

public record CreateLookUpKey : IRequest<int> {
    public string Name { get; init; }

    public CreateLookUpKey(string name){
        this.Name = name;
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
            Name = request.Name,
            Type = "key",
        };

        await _context.Lookups.AddAsync(newLookup);
        await _context.SaveChangesAsync(cancellationToken);
        return newLookup.Id;
        
    }
}