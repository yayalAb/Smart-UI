using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PortModule.Queries.GetPort;

public record GetPort : IRequest<Port> {
    public int Id {get; init;}
}

public class GetPortHandler : IRequestHandler<GetPort, Port> {

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;

    public GetPortHandler(
        IIdentityService identityService,
        IAppDbContext context
    ){
        _identityService = identityService;
        _context = context;
    }

    public async Task<Port> Handle(GetPort request, CancellationToken cancellationToken) {
        
        var port = await _context.Ports.Where(t => t.Id == request.Id).FirstOrDefaultAsync();
        if(port == null) {
            throw new Exception("port not found!");
        }

        return port;

    }

}