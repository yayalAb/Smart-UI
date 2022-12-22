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
        public DateTime Date {get; set;}

    }

    public class AddCurrencyRateHandler : IRequestHandler<AddCurrencyRate, CustomResponse> {
        private readonly IAppDbContext _context;
        private readonly CurrencyConversionService _currencyService;

        public AddCurrencyRateHandler(IAppDbContext context, CurrencyConversionService currencyService)
        {
            _context = context;
            _currencyService = currencyService;
        }

        public async Task<CustomResponse> Handle(AddCurrencyRate request, CancellationToken cancellationToken) {

            var currency = await _context.Units.Where(u => u.Currency == request.Currency && u.Date.Date == request.Date.Date).FirstOrDefaultAsync();

            if(currency != null){
                currency.Rate = request.Rate;
                await _context.SaveChangesAsync(cancellationToken);
                return CustomResponse.Succeeded("Currency rate updated");
            }else{

                _context.Units.Add(new Domain.Entities.CurrencyConversion{
                    Currency = request.Currency,
                    Rate = request.Rate,
                    Date = request.Date.Date
                });

                await _context.SaveChangesAsync(cancellationToken);
                return CustomResponse.Succeeded("Currency rate saved");

            }

        }
        
    }



}