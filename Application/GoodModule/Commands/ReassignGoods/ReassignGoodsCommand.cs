

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
            return await executionStrategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.database.BeginTransaction())
                {
                    try
                    {
                        var container = request.ContainerId != null ? await _context.Containers.FindAsync(request.ContainerId) : null;
                        var good_id_list = from gs in request.Goods select gs.Id;
                        var containers_tobe_updated = new List<Container>();

                        if (container != null)
                        {
                            containers_tobe_updated.Add(container);
                        }

                        var goods = await _context.Goods.AsNoTracking().Where(g => good_id_list.Contains(g.Id))
                        .Include(g => g.Container)
                        .Select(g => new Good
                        {
                            Id = g.Id,
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
                            ContainerId = g.ContainerId,
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
                                Currency = g.Container.Currency,
                                Location = g.Container.Location,
                                LocationPortId = g.Container.LocationPortId,
                                IsAssigned = g.Container.IsAssigned,
                                OperationId = g.Container.OperationId,
                                GeneratedDocumentId = g.Container.GeneratedDocumentId
                            }
                        }).ToListAsync();

                        foreach (ReassignedGoodDto gd in request.Goods)
                        {

                            var selectedGood = goods.Find(g => g.Id == gd.Id);

                            if (selectedGood == null)
                            {
                                continue;
                            }

                            var remained = selectedGood.RemainingQuantity - gd.Quantity;
                            var calculated_weight = ((selectedGood.Weight / selectedGood.Quantity) * gd.Quantity);
                            var container_to_be_updated = selectedGood.Container;
                            selectedGood.Container = null;


                            //making goods unstaffed
                            if (container == null)
                            {

                                if (container_to_be_updated != null) {

                                    //update the good
                                    if (remained > 0) {
                                        _context.Goods.Add(new Good {
                                            Description = selectedGood.Description,
                                            HSCode = selectedGood.HSCode,
                                            Manufacturer = selectedGood.Manufacturer,
                                            Weight = calculated_weight,
                                            WeightUnit = selectedGood.WeightUnit,
                                            Quantity = gd.Quantity,
                                            RemainingQuantity = gd.Quantity,
                                            Type = "Unstaff",
                                            Location = selectedGood.Location,
                                            ChasisNumber = selectedGood.ChasisNumber,
                                            EngineNumber = selectedGood.EngineNumber,
                                            ModelCode = selectedGood.ModelCode,
                                            IsAssigned = selectedGood.IsAssigned,
                                            Unit = selectedGood.Unit,
                                            UnitPrice = selectedGood.UnitPrice,
                                            CBM = selectedGood.CBM,
                                            OperationId = selectedGood.OperationId,
                                            LocationPortId = selectedGood.LocationPortId
                                        });

                                        selectedGood.RemainingQuantity = remained;
                                        selectedGood.Weight -= calculated_weight;

                                    } else {

                                        selectedGood.ContainerId = null;
                                        selectedGood.Type = "Unstaff";

                                    }

                                    _context.Goods.Update(selectedGood);

                                    //update container
                                    var temp_container = containers_tobe_updated.FirstOrDefault(c => c.Id == container_to_be_updated.Id);
                                    if (temp_container != null)
                                    {
                                        temp_container.GrossWeight -= AppdivConvertor.WeightConversion(selectedGood.WeightUnit, calculated_weight, temp_container.WeightMeasurement);
                                        temp_container.TotalPrice -= AppdivConvertor.CurrencyConversion(selectedGood.Unit, (selectedGood.UnitPrice * gd.Quantity), temp_container.Currency);
                                        temp_container.Quantity -= 1;
                                    }
                                    else
                                    {
                                        container_to_be_updated.GrossWeight -= AppdivConvertor.WeightConversion(selectedGood.WeightUnit, calculated_weight, container_to_be_updated.WeightMeasurement);
                                        container_to_be_updated.TotalPrice -= AppdivConvertor.CurrencyConversion(selectedGood.Unit, (selectedGood.UnitPrice * gd.Quantity), container_to_be_updated.Currency);
                                        container_to_be_updated.Quantity -= 1;
                                        containers_tobe_updated.Add(container_to_be_updated);
                                    }

                                    

                                }

                                continue;

                            }

                            //changing goods container
                            else {

                                //update the good
                                if (remained > 0)
                                {

                                    selectedGood.RemainingQuantity = remained;
                                    selectedGood.Weight -= calculated_weight;
                                    _context.Goods.Update(selectedGood);

                                    _context.Goods.Add(new Good
                                    {
                                        Description = selectedGood.Description,
                                        HSCode = selectedGood.HSCode,
                                        Manufacturer = selectedGood.Manufacturer,
                                        Weight = calculated_weight,
                                        WeightUnit = selectedGood.WeightUnit,
                                        Quantity = gd.Quantity,
                                        RemainingQuantity = gd.Quantity,
                                        Type = "Container",
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
                                    _context.Goods.Update(selectedGood);
                                }


                                //check if the good is contained or unstafed
                                //updating the container
                                if (container_to_be_updated != null) {

                                    var temp_container = containers_tobe_updated.FirstOrDefault(c => c.Id == container_to_be_updated.Id);
                                    if (temp_container != null)
                                    {
                                        temp_container.GrossWeight -= AppdivConvertor.WeightConversion(selectedGood.WeightUnit, calculated_weight, temp_container.WeightMeasurement);
                                        temp_container.TotalPrice -= AppdivConvertor.CurrencyConversion(selectedGood.Unit, (selectedGood.UnitPrice * gd.Quantity), temp_container.Currency);
                                    }
                                    else
                                    {
                                        container_to_be_updated.GrossWeight -= AppdivConvertor.WeightConversion(selectedGood.WeightUnit, calculated_weight, container_to_be_updated.WeightMeasurement);
                                        container_to_be_updated.TotalPrice -= AppdivConvertor.CurrencyConversion(selectedGood.Unit, (selectedGood.UnitPrice * gd.Quantity), container_to_be_updated.Currency);
                                        containers_tobe_updated.Add(container_to_be_updated);
                                    }

                                }

                                container.Quantity += gd.Quantity;
                                container.GrossWeight += AppdivConvertor.WeightConversion(selectedGood.WeightUnit, calculated_weight, container.WeightMeasurement);
                                container.TotalPrice += AppdivConvertor.CurrencyConversion(selectedGood.Unit, (selectedGood.UnitPrice * gd.Quantity), container.Currency);

                            }

                        }

                        _context.Containers.UpdateRange(containers_tobe_updated);
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