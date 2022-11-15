using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.LookUp.Query.GetByKey {
    public class GetLookUpByKey : IRequest<ICollection<Lookup>> {
        public string Key {get; init;}

        public GetLookUpByKey(string type){
            this.Key = type;
        }
    }

    public class GetLookUpByKeyHandler : IRequestHandler<GetLookUpByKey, ICollection<Lookup>> {
        private readonly IAppDbContext _context;

        public GetLookUpByKeyHandler(IAppDbContext context) {
            _context = context;
        }
        public async Task<ICollection<Lookup>> Handle(GetLookUpByKey request, CancellationToken cancellationToken) {
            // return await _context.Lookups.Where(l => l.Key == request.Key).ToListAsync();
            return await _context.Lookups.ToListAsync();

        }
    }
}