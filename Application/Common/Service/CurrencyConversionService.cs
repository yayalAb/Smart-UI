using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.Units;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Service;
public class CurrencyConversionService
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public CurrencyConversionService(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CurrencyConversion> getCurrency(string currencyName, DateTime date) {
        
        var unit = await _context.Units.Where(u => u.Currency == currencyName && u.Date == date.Date).FirstOrDefaultAsync();
        if(unit == null) {
            throw new Exception($"currency {currencyName} for today not found!");
        }
        
        return unit;

    }

    public async Task<CustomResponse> addCurrencyRate(string currencyName, Double rate, CancellationToken cancellationToken){
        
        _context.Units.Add(new CurrencyConversion {
            Currency = currencyName,
            Rate = rate,
            Date = DateTime.Today
        });

        await _context.SaveChangesAsync(cancellationToken);

        return CustomResponse.Succeeded("Currency added successfully");

    }

    public async Task<Double> convert(string from, Double value, string to, DateTime date) {

        var convertFrom = await getCurrency(from, date);
        
        var to_default = value / convertFrom.Rate;

        if (to == null) {
            return to_default;
        } else {

            var convertTo = await getCurrency(to, date);

            if (convertTo.Currency == Currency.Default.name) {
                return to_default;
            } else {
                return to_default * convertTo.Rate;
            }

        }

    }


}