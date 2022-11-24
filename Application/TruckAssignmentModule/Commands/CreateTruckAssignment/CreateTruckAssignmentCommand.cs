
using MediatR;
using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;

namespace Application.TruckAssignmentModule.Commands.CreateTruckAssignment
{

    public record CreateTruckAssignmentCommand : IRequest<CustomResponse>
    {
        public int OperationId { get; set; }
        public int DriverId { get; set; }
        public int TruckId { get; set; }
        public int SourcePortId { get; set; }
        public int DestinationPortId { get; set; }
        public List<int>? ContainerIds { get; set; }
        public List<int>? GoodIds { get; set; }




    }

    public class CreateTruckAssignmentCommandHandler : IRequestHandler<CreateTruckAssignmentCommand, CustomResponse>
    {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateTruckAssignmentCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateTruckAssignmentCommandHandler(
            IIdentityService identityService,
            IAppDbContext context,
            ILogger<CreateTruckAssignmentCommandHandler> logger,
            IMapper mapper
        )
        {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CustomResponse> Handle(CreateTruckAssignmentCommand request, CancellationToken cancellationToken)
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
                            SourcePortId = request.SourcePortId,
                            DestinationPortId = request.DestinationPortId,
                            Containers = containers,
                            Goods = goods
                            
                        };
                        await _context.TruckAssignments.AddAsync(newTruckAssignment);
                        await _context.SaveChangesAsync(cancellationToken);

                        await transaction.CommitAsync();
                        return CustomResponse.Succeeded("truck assignment created successfully", 201);

                    }
                    catch (System.Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }




                }
            });

        }

    }

};