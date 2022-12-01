
using MediatR;
using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;
using Domain.Enums;

namespace Application.GoodModule.Commands.AssignGoodsCommand
{

    public record AssignGoodsCommand : IRequest<CustomResponse>
    {
        public int OperationId { get; set; }
        public List<ASgContainerDto>? Containers { get; set; }
        public List<GoodDto>? Goods { get; set; }



    }

    public class AssignGoodsCommandHandler : IRequestHandler<AssignGoodsCommand, CustomResponse>
    {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<AssignGoodsCommandHandler> _logger;
        private readonly IMapper _mapper;

        public AssignGoodsCommandHandler(
            IIdentityService identityService,
            IAppDbContext context,
            ILogger<AssignGoodsCommandHandler> logger,
            IMapper mapper
        )
        {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CustomResponse> Handle(AssignGoodsCommand request, CancellationToken cancellationToken)
        {

            var executionStrategy = _context.database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.database.BeginTransaction())
                {
                    try
                    {
                        List<Good> goods = _mapper.Map<List<Good>>(request.Goods);
                        goods.ForEach(good => good.OperationId = request.OperationId);

                        if (request.Containers != null)
                        {
                            List<Container> containers = _mapper.Map<List<Container>>(request.Containers);
                            containers.ForEach(container =>{

                                container.OperationId = request.OperationId;
                                container.Goods.ToList().ForEach(good =>{
                                    good.OperationId = request.OperationId;
                                    good.ContainerId = container.Id;
                                    good.Location = container.Location;
                                });
                            });
                            await _context.Containers.AddRangeAsync(containers);
                            await _context.SaveChangesAsync(cancellationToken);

                            // goods.ForEach(good =>
                            // {
                            //     if (good.Type == Enum.GetName(typeof(GoodType), GoodType.Container))
                            //     {
                            //         good.ContainerId = container.Id;
                            //         good.Location = container.Location;
                            //     }

                            // });

                        }

                        if (goods.Any(good => good.Location == null))
                        {
                            throw new GhionException(CustomResponse.BadRequest("location of goods can not be null if container is not provided"));
                        }

                        await _context.AddRangeAsync(goods);
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync();
                        return CustomResponse.Succeeded("goods  assigned successfully", 201);

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