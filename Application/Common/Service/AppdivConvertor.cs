
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
    this method will convert any weight units to the default weight unit if convertTo unit has not been set
    */
    public static float WeightConversion(string unitName, float value, string? convertTo) {
        Unit convertFrom = WeightUnits.getUnit(unitName);
        var to_default = value/convertFrom.rate;
        Unit toBeConverted = WeightUnits.getUnit(convertTo);
        if(toBeConverted.Equals(WeightUnits.Default)){
            return to_default;
        }else{
            return to_default * toBeConverted.rate; 
        }
    }

    /**
    this method will convert any currency to the default currency if convertTo currency has not been set
    */
    public static float CurrencyConversion(string unitName, float value, string? convertTo) {

        Unit convertFrom = Currency.getUnit(unitName);
        var toBeConverted = convertTo == null ? null : Currency.getUnit(convertTo);
        var to_default = value/convertFrom.rate;

        if(toBeConverted == null) {
            return to_default;
        }else{
            if(toBeConverted.Equals(Currency.Default)) {
                return to_default;
            } else {
                return to_default * toBeConverted.rate; 
            }
        }

    }

}