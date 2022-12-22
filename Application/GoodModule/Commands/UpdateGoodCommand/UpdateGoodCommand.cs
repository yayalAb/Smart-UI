
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Service;
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
        private readonly CurrencyConversionService _currencyService;

        public UpdateGoodCommandHandler(
            IIdentityService identityService,
            IAppDbContext context,
            ILogger<UpdateGoodCommandHandler> logger,
            IMapper mapper,
            CurrencyConversionService currencyService
        )
        {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _currencyService = currencyService;
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
                operation.Containers = _mapper.Map<ICollection<Container>>(request.Containers);
            if (request.Containers != null)
            {
                List<string> sh_codes = new List<string>();
                foreach (var container in operation.Containers.ToList())
                {

                    container.OperationId = request.OperationId;
                    container.Article = 1;
                    container.Quantity = container.Goods.Count;
                    container.WeightMeasurement = container.WeightMeasurement;
                    container.Currency = container.Currency;
                    foreach (var good in container.Goods.ToList())
                    {
                        good.OperationId = request.OperationId;
                        good.Location = container.Location;
                        good.ContainerId = container.Id;
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

            }
            operation.Goods = _mapper.Map<ICollection<Good>>(request.Goods);
            if (request.Goods != null)
            {
                operation.Goods.ToList().ForEach(
                    g =>
                    {
                        g.OperationId = request.OperationId;
                        g.RemainingQuantity = g.Quantity;

                    });

            }
            _context.Operations.Update(operation);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("assign goods updated successfully");
        }

    }

}