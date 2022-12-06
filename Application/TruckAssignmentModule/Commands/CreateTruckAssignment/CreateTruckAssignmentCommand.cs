
using MediatR;
using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;
using Application.OperationDocuments.Queries.GenerateGatepass;

namespace Application.TruckAssignmentModule.Commands.CreateTruckAssignment
{

    public record CreateTruckAssignmentCommand : IRequest<GatepassDto>
    {
        public int OperationId { get; set; }
        public int DriverId { get; set; }
        public int TruckId { get; set; }
        public string SourceLocation { get; set; }
        public string DestinationLocation {get; set;}
        public int? SourcePortId { get; set; }
        public int? DestinationPortId { get; set; }
        public string? TransportationMethod { get; set; }
        public List<int>? ContainerIds { get; set; }
        public List<int>? GoodIds { get; set; }




    }

    public class CreateTruckAssignmentCommandHandler : IRequestHandler<CreateTruckAssignmentCommand, GatepassDto>
    {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateTruckAssignmentCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly GatepassService _gatepassService;

        public CreateTruckAssignmentCommandHandler(
            IIdentityService identityService,
            IAppDbContext context,
            ILogger<CreateTruckAssignmentCommandHandler> logger,
            IMapper mapper,
            GatepassService gatepassService
        )
        {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _gatepassService = gatepassService;
        }

        public async Task<GatepassDto> Handle(CreateTruckAssignmentCommand request, CancellationToken cancellationToken)
        {

            var executionStrategy = _context.database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.database.BeginTransaction())
                {
                    try
                    {
                        List<Container> containers = new List<Container>();
                        List<Good> goods = new List<Good>();
                        if (request.ContainerIds != null)
                        {
                            for (int i = 0; i < request.ContainerIds.Count; i++)
                            {
                                var container = await _context.Containers.FindAsync(request.ContainerIds[i]);
                                if (container == null)
                                {
                                    throw new GhionException(CustomResponse.BadRequest($"container with id = {request.ContainerIds[i]}  is not found"));
                                }
                                containers.Add(container);
                            }
                        }
                        if (request.GoodIds != null)
                        {
                            for (int j = 0; j < request.GoodIds.Count; j++)
                            {
                                var good = await _context.Goods.FindAsync(request.GoodIds[j]);
                                if (good == null)
                                {
                                    throw new GhionException(CustomResponse.BadRequest($"good with id = {request.GoodIds[j]}  is not found"));
                                }

                                goods.Add(good);
                            }
                        }
                        //Create new TruckAssignment
                        TruckAssignment newTruckAssignment = new TruckAssignment
                        {
                            OperationId = request.OperationId,
                            DriverId = request.DriverId,
                            TruckId = request.TruckId,
                            SourceLocation = request.SourceLocation,
                            DestinationLocation = request.DestinationLocation,
                            SourcePortId = request.SourcePortId,
                            DestinationPortId = request.DestinationPortId,
                            Containers = containers,
                            Goods = goods
                            
                        };
                        await _context.TruckAssignments.AddAsync(newTruckAssignment);
                        await _context.SaveChangesAsync(cancellationToken);
                        await ChangeIsAssignedFlag(request.TruckId , request.DriverId , cancellationToken);
                       // generate gatepass (fetch gatepass data)
                        var gatepass =  await _gatepassService.GenerateGatePass(request.OperationId , newTruckAssignment.Id , cancellationToken);
                        await transaction.CommitAsync();
                        
                        return gatepass;

                    }
                    catch (System.Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            });

        }

        private async Task ChangeIsAssignedFlag(int truckId , int driverId , CancellationToken cancellationToken){
            var truck = await _context.Trucks.FindAsync(truckId);

            truck!.IsAssigned = true; 
            _context.Trucks.Update(truck);
            await _context.SaveChangesAsync(cancellationToken);

            var driver = await _context.Drivers.FindAsync(driverId);
            driver!.IsAssigned = true; 
             _context.Drivers.Update(driver);
             await _context.SaveChangesAsync(cancellationToken);



        }

    }

};