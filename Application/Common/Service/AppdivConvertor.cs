
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

        var property = (Unit) typeof(T).GetProperty(name).GetValue(null, null);
        
        if(property == null){
            throw new GhionException(CustomResponse.Failed($"Weight Unit 'name' not found"));
        }

        return property;
    }
}