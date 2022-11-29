
using MediatR;
using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;

namespace Application.TruckAssignmentModule.Commands.UpdateTruckAssignment
{

    public record UpdateTruckAssignmentCommand : IRequest<CustomResponse>
    {
        public int Id { get; set; }
        public int OperationId { get; set; }
        public int DriverId { get; set; }
        public int TruckId { get; set; }
        public string SourceLocation { get; set; }
        public string DestinationLocation { get; set; }
        public int? SourcePortId { get; set; }
        public int? DestinationPortId { get; set; }
        public List<int>? ContainerIds { get; set; }
        public List<int>? GoodIds { get; set; }




    }

    public class UpdateTruckAssignmentCommandHandler : IRequestHandler<UpdateTruckAssignmentCommand, CustomResponse>
    {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateTruckAssignmentCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateTruckAssignmentCommandHandler(
            IIdentityService identityService,
            IAppDbContext context,
            ILogger<UpdateTruckAssignmentCommandHandler> logger,
            IMapper mapper
        )
        {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CustomResponse> Handle(UpdateTruckAssignmentCommand request, CancellationToken cancellationToken)
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

                        //fetch existing truck assignment 
                        var existingTruckAssignment = await _context.TruckAssignments.FindAsync(request.Id);
                        if (existingTruckAssignment == null)
                        {
                            throw new GhionException(CustomResponse.BadRequest($"TruckAssignment with id = {request.Id} is not found"));
                        }

                        //fetch containers
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

                        //fetch goods
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
                        //Update  TruckAssignment

                        existingTruckAssignment.OperationId = request.OperationId;
                        existingTruckAssignment.DriverId = request.DriverId;
                        existingTruckAssignment.TruckId = request.TruckId;
                        existingTruckAssignment.SourcePortId = request.SourcePortId;
                        existingTruckAssignment.DestinationPortId = request.DestinationPortId;
                        existingTruckAssignment.Containers = containers;
                        existingTruckAssignment.Goods = goods;


                        _context.TruckAssignments.Update(existingTruckAssignment);
                        await _context.SaveChangesAsync(cancellationToken);

                        await transaction.CommitAsync();
                        return CustomResponse.Succeeded("truck assignment Updated successfully");

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