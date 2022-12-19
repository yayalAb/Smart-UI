
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.GoodModule.Commands.UpdateGoodCommand
{

    public record UpdateGoodCommand : IRequest<CustomResponse>
    {
        public int OperationId { get; set; }
        public List<UpdateGoodContainerDto>? Containers { get; set; }
        public List<UpdateGoodDto>? Goods { get; set; }

    }



    public class UpdateGoodCommandHandler : IRequestHandler<UpdateGoodCommand, CustomResponse>
    {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateGoodCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateGoodCommandHandler(
            IIdentityService identityService,
            IAppDbContext context,
            ILogger<UpdateGoodCommandHandler> logger,
            IMapper mapper
        )
        {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CustomResponse> Handle(UpdateGoodCommand request, CancellationToken cancellationToken)
        {

            var operation = await _context.Operations
                  .Include(o => o.Containers)!
                      .ThenInclude(c => c.Goods)
                  .Include(o => o.Goods)
                  .Where(o => o.Id == request.OperationId).FirstOrDefaultAsync();

            if (operation == null)
            {
                throw new GhionException(CustomResponse.NotFound($"operation with id = {request.OperationId} is not found"));
            }
            if (request.Containers != null)
            {
                operation.Containers = _mapper.Map<ICollection<Container>>(request.Containers);
                operation.Containers.ToList().ForEach(container =>
                {
                    container.OperationId = request.OperationId;
                    container.Goods.ToList().ForEach(good =>
                    {
                        good.OperationId = request.OperationId;
                        good.Location = container.Location;
                        good.ContainerId = container.Id;
                    });
                });
            }
            if (request.Goods != null)
            {
                operation.Goods = _mapper.Map<ICollection<Good>>(request.Goods);
                operation.Goods.ToList().ForEach(g => g.OperationId = request.OperationId);

            }
            _context.Operations.Update(operation);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("assign goods updated successfully");
        }

    }

}