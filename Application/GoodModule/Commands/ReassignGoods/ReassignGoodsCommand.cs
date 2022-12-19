

using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Service;
using Application.GoodModule.Commands.ReassignGoods;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.GoodModule.Commands.AssignGoodsCommand
{

    public record ReassignGoodsCommand : IRequest<CustomResponse>
    {
        public int? ContainerId { get; init; }
        public ICollection<ReassignedGoodDto> Goods { get; init; }

    }

    public class ReassignGoodsCommandHandler : IRequestHandler<ReassignGoodsCommand, CustomResponse>
    {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<AssignGoodsCommandHandler> _logger;
        private readonly IMapper _mapper;

        public ReassignGoodsCommandHandler(
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

        public async Task<CustomResponse> Handle(ReassignGoodsCommand request, CancellationToken cancellationToken)
        {

            var executionStrategy = _context.database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(async () => {
                using (var transaction = _context.database.BeginTransaction()) {
                    try {
                        var container = await _context.Containers.FindAsync(request.ContainerId);
                        var good_id_list = from gs in request.Goods select gs.Id;

                        var goods = await _context.Goods.Where(g => good_id_list.Contains(g.Id))
                        .Include(g => g.Container)
                        .Select(g => new Good {
                            Description = g.Description,
                            HSCode = g.HSCode,
                            Manufacturer = g.Manufacturer,
                            Weight = g.Weight,
                            WeightUnit = g.WeightUnit,
                            Quantity = g.Quantity,
                            RemainingQuantity = g.RemainingQuantity,
                            Type = g.Type,
                            Location = g.Location,
                            ChasisNumber = g.ChasisNumber,
                            EngineNumber = g.EngineNumber,
                            ModelCode = g.ModelCode,
                            IsAssigned = g.IsAssigned,
                            Unit = g.Unit,
                            UnitPrice = g.UnitPrice,
                            CBM = g.CBM,
                            ContainerId = request.ContainerId,
                            OperationId = g.OperationId,
                            LocationPortId = g.LocationPortId,
                            Container = (g.Container == null) ? null : new Container
                            {
                                Id = g.Container.Id,
                                ContianerNumber = g.Container.ContianerNumber,
                                GoodsDescription = g.Container.GoodsDescription,
                                SealNumber = g.Container.SealNumber,
                                Article = g.Container.Article,
                                Size = g.Container.Size,
                                GrossWeight = g.Container.GrossWeight,
                                WeightMeasurement = g.Container.WeightMeasurement,
                                Quantity = g.Container.Quantity,
                                TotalPrice = g.Container.TotalPrice,
                                Currency = g.Container.Currency
                            }
                        }).ToListAsync();

                        foreach (ReassignedGoodDto gd in request.Goods)
                        // foreach (Good selectedGood in goods)
                        {

                            var selectedGood = goods.Find(g => g.Id == gd.Id);
                            // var gd = request.Goods.ToList().Find(g => g.Id == selectedGood.Id);

                            if (selectedGood == null) {
                                continue;
                            }

                            var remained = selectedGood.Quantity - gd.Quantity;
                            var calculated_weight = ((selectedGood.Weight / selectedGood.Quantity) * gd.Quantity);

                            if (container == null)
                            {

                                if (selectedGood.Container != null)
                                {
                                    var container_to_be_updated = selectedGood.Container;
                                    selectedGood.Container = null;
                                    selectedGood.ContainerId = null;
                                    container_to_be_updated.GrossWeight -= AppdivConvertor.WeightConversion(selectedGood.WeightUnit, calculated_weight, container_to_be_updated.WeightMeasurement);
                                    container_to_be_updated.TotalPrice -= AppdivConvertor.CurrencyConversion(selectedGood.Unit, (selectedGood.UnitPrice * gd.Quantity), container_to_be_updated.Currency);
                                    container_to_be_updated.Quantity -= 1;
                                    _context.Containers.Update(container_to_be_updated);
                                }

                                continue;

                            }

                            if (remained > 0)
                            {

                                selectedGood.RemainingQuantity = remained;

                                _context.Goods.Add(new Good
                                {
                                    Description = selectedGood.Description,
                                    HSCode = selectedGood.HSCode,
                                    Manufacturer = selectedGood.Manufacturer,
                                    Weight = calculated_weight,
                                    WeightUnit = selectedGood.WeightUnit,
                                    Quantity = gd.Quantity,
                                    RemainingQuantity = gd.Quantity,
                                    Type = selectedGood.Type,
                                    Location = selectedGood.Location,
                                    ChasisNumber = selectedGood.ChasisNumber,
                                    EngineNumber = selectedGood.EngineNumber,
                                    ModelCode = selectedGood.ModelCode,
                                    IsAssigned = selectedGood.IsAssigned,
                                    Unit = selectedGood.Unit,
                                    UnitPrice = selectedGood.UnitPrice,
                                    CBM = selectedGood.CBM,
                                    ContainerId = request.ContainerId,
                                    OperationId = selectedGood.OperationId,
                                    LocationPortId = selectedGood.LocationPortId
                                });

                            }
                            else
                            {
                                selectedGood.ContainerId = request.ContainerId;
                                selectedGood.Container = null;
                            }

                            //check if the good is contained or unstafed
                            if (selectedGood.Container != null)
                            {
                                selectedGood.Container.GrossWeight -= AppdivConvertor.WeightConversion(selectedGood.WeightUnit, calculated_weight, selectedGood.Container.Currency);
                                selectedGood.Container.TotalPrice -= AppdivConvertor.CurrencyConversion(selectedGood.Unit, (selectedGood.UnitPrice * gd.Quantity), selectedGood.Container.Currency);
                            }

                            if (container != null)
                            {
                                container.Quantity += gd.Quantity;
                                container.GrossWeight += calculated_weight;
                                container.TotalPrice += AppdivConvertor.CurrencyConversion(selectedGood.Unit, (selectedGood.UnitPrice * gd.Quantity), container.Currency);
                            }

                        }

                        _context.Goods.UpdateRange(goods);
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync();

                        return CustomResponse.Succeeded("Goods are reassigned");
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            });


        }

    }

};