
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Service;
using AutoMapper;
using Domain.Common.Units;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
        private readonly CurrencyConversionService _currencyService;
        private readonly ILogger<AssignGoodsCommandHandler> _logger;
        private readonly IMapper _mapper;

        public AssignGoodsCommandHandler(
            IIdentityService identityService,
            IAppDbContext context,
            CurrencyConversionService currencyService,
            ILogger<AssignGoodsCommandHandler> logger,
            IMapper mapper
        )
        {
            _identityService = identityService;
            _context = context;
            _currencyService = currencyService;
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
                        if (goods != null && goods.Count != 0)
                        {

                            goods.ForEach(good =>
                            {
                                good.OperationId = request.OperationId;
                                good.RemainingQuantity = good.Quantity;
                            });

                            if (goods.Any(good => good.Location == null))
                            {
                                throw new GhionException(CustomResponse.BadRequest("location of goods can not be null if container is not provided"));
                            }
                            await _context.AddRangeAsync(goods);
                            await _context.SaveChangesAsync(cancellationToken);
                        }

                        if (request.Containers != null)
                        {

                            List<string> sh_codes = new List<string>();
                            List<Container> containers = _mapper.Map<List<Container>>(request.Containers);
                            foreach (var container in containers)
                            {

                                container.OperationId = request.OperationId;
                                container.Article = 1;
                                container.Quantity = container.Goods.Count;
                                container.WeightMeasurement = container.WeightMeasurement;
                                container.Currency = container.Currency;
                                foreach (var good in container.Goods)
                                {
                                    good.OperationId = request.OperationId;
                                    good.ContainerId = container.Id;
                                    good.Location = container.Location;
                                    good.RemainingQuantity = good.Quantity;
                                    if (!sh_codes.Any(code => code == good.HSCode))
                                    {
                                        sh_codes.Add(good.HSCode);
                                        container.Article += 1;
                                    }
                                    container.TotalPrice += (float)(await _currencyService.convert(good.Unit, good.UnitPrice, container.Currency, DateTime.Today) * good.Quantity);
                                    container.GrossWeight += AppdivConvertor.WeightConversion(good.WeightUnit, good.Weight);
                                }

                            }
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