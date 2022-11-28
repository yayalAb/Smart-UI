
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationModule.Queries.OperationDashboard;

public record OperationCountGraph : IRequest<ICollection<OperationCountPerMonthDto>> {
    public int year {get; set;}
}

public class OperationCountGraphHandler : IRequestHandler<OperationCountGraph, ICollection<OperationCountPerMonthDto>> {

    private readonly IAppDbContext _context;

    public OperationCountGraphHandler(IAppDbContext context){
        _context = context;
    }

    public async Task<ICollection<OperationCountPerMonthDto>> Handle(OperationCountGraph request, CancellationToken cancellationToken) {
        
        List<OperationCountPerMonthDto> graph = new List<OperationCountPerMonthDto>();

        for(int i = 1; i <= 12; i++){
            graph.Add(await Monthly(i, request.year));
        }

        return graph;

    }

    public async Task<OperationCountPerMonthDto> Monthly(int month, int year) {
        
        var count = await _context.Operations
                                    .Where(o => o.Status != "closed" && o.Created >= new DateTime(year, month, 1) && o.Created < new DateTime((month == 12 ? year + 1 : year), (month == 12 ? 1 : month), 1))
                                    .ToListAsync();
        
        return new OperationCountPerMonthDto {
            Month = month,
            Count = count.Count
        };

    }

}