using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Common.Service;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.SettingModule.Command.CreateTodayCurrencyRate {
    public record AddCurrencyRate : IRequest<CustomResponse>
    {

        public string Currency { get; init; }
        public double Rate { get; init; }

    }

    public class AddCurrencyRateHandler : IRequestHandler<AddCurrencyRate, CustomResponse>
    {
        private readonly IAppDbContext _context;
        private readonly CurrencyConversionService _currencyService;

        public AddCurrencyRateHandler(IAppDbContext context, CurrencyConversionService currencyService)
        {
            _context = context;
            _currencyService = currencyService;
        }

        public async Task<CustomResponse> Handle(AddCurrencyRate request, CancellationToken cancellationToken)
        {
            _context.Units.Add(new Domain.Entities.CurrencyConversion{ Currency = request.Currency, Rate = request.Rate});
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Currency rate saved");
        }
        
    }



}