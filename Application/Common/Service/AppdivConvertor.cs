
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Common.Units;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Common.Service;
public class AppdivConvertor
{
    // private readonly IAppDbContext _context;
    // private readonly IMapper _mapper;

    // public AppdivConvertor(IAppDbContext context, IMapper mapper) {
    //     _context = context;
    //     _mapper = mapper;
    // }

    /**
    this method will convert any weight units to the default weight unit
    */
    public static Double WeightConversion(string unitName, float value) {
        Unit toBeConverted = AppdivConvertor.unitFactory<WeightUnits>(unitName);
        return value/toBeConverted.rate;
    }

    public static float CurrencyConversion(string unitName, float value) {
        Unit toBeConverted = AppdivConvertor.unitFactory<Currency>(unitName);
        return (float) value/toBeConverted.rate;
    }

    // type can be weight or price
    // name changes based on the type
    public static Unit unitFactory<T>(string name) {
        // if(T.Units.Contains(name)){
        //     throw new GhionException(CustomResponse.Failed($"Weight Unit 'name' not found"));
        // }

        var property = (Unit) typeof(T).GetProperty(name).GetValue(null, null);
        
        if(property == null){
            throw new GhionException(CustomResponse.Failed($"Weight Unit 'name' not found"));
        }

        return property;
        // if(name == WeightUnits.KG.name){
        //     return WeightUnits.KG;
        // }else if(name == WeightUnits.Ton.name){
        //     return WeightUnits;
        // }else if(name == WeightUnits.Ton.name){
        //     return WeightUnits.KG;
        // }
    }
}