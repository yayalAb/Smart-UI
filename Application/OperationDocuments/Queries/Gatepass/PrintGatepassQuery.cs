
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationDocuments.Queries.Gatepass.GPDtos;
using Application.OperationFollowupModule;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.OperationDocuments.Queries.Gatepass;

public record PrintGatepassQuery : IRequest<GPTruckAssignmentDto>
{
    public int TruckAssignmentId { get; set; }
}
public class PrintGatepassQueryHandler : IRequestHandler<PrintGatepassQuery, GPTruckAssignmentDto>
{

    private readonly IAppDbContext _context;
    private readonly ILogger logger;
    private readonly OperationEventHandler _operationEventHandler;
    public PrintGatepassQueryHandler(IAppDbContext context, ILogger<PrintGatepassQuery> logger, OperationEventHandler operationEventHandler)
    {
        _context = context;
        this.logger = logger;
        _operationEventHandler = operationEventHandler;
    }

    public async Task<GPTruckAssignmentDto> Handle(PrintGatepassQuery request, CancellationToken cancellationToken)
    {

        //fetch gatepass form data 
        var data = await _context.TruckAssignments
           .Where(ta => ta.Id == request.TruckAssignmentId)
           .Include(ta => ta.Containers)!
               .ThenInclude(c => c.Goods)
           .Include(ta => ta.Truck)
           .Include(ta => ta.Goods)
           .Include(ta => ta.Containers)
           .Include(ta => ta.Operation)
           .Select(ta => new GPTruckAssignmentDto
           {
               Operation = new GPOperationDto
               {
                   Localization = ta.Operation.Localization,
                   SNumber = ta.Operation.SNumber
               },
               Good = ta.Goods == null ? null : ta.Goods.Select(g => new GPGoodDto
               {
                   Weight = g.Weight,
                   Quantity = g.Quantity,
                   ContainerNumber = g.Container == null ? null : g.Container.ContianerNumber,
               }).ToList(),
               TruckNumber = ta.Truck.TruckNumber
           }).FirstAsync();

        if (data == null)
        {
            throw new GhionException(CustomResponse.NotFound("There is no TruckAssignment with the given Id!"));
        }

        return data;

    }



}