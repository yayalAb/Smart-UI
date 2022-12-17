
using Application.Common.Interfaces;
using MediatR;

namespace Application.OperationModule.Queries.OperationDashboard;

public record OperationDashboardInfo : IRequest<OperationDashboardDto> { }

public class OperationDashboardHandler : IRequestHandler<OperationDashboardInfo, OperationDashboardDto>
{

    private readonly IAppDbContext _context;

    public OperationDashboardHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationDashboardDto> Handle(OperationDashboardInfo request, CancellationToken cancellationToken)
    {

        var totalOpenedCount = _context.Operations.Where(o => o.Status != "closed").ToList().Count;
        var totalClosedCount = _context.Operations.Where(o => o.Status == "closed").ToList().Count;
        return new OperationDashboardDto
        {
            Closed = totalClosedCount,
            Opened = totalOpenedCount
        };
    }

}