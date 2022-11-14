using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.LookUp.Query.GetByKey {
    public class GetLookUpByKey : IRequest<List<Lookup>> {
        public string Key {get; init;}

        public GetLookUpByKey(string type){
            this.Key = type;
        }
    }

    public class GetLookUpByKeyHandler : IRequestHandler<GetLookUpByKey, List<Lookup>> {
        private readonly IAppDbContext _context;

        public GetLookUpByKeyHandler(IAppDbContext context) {
            _context = context;
        }
        public async Task<List<Lookup>> Handle(GetLookUpByKey request, CancellationToken cancellationToken) {
            return await _context.Lookups.Where(l => l.Key.Equals(request.Key)).ToListAsync();
        }
    }
}