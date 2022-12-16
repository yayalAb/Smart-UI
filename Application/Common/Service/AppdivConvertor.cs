
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
        Unit toBeConverted = WeightUnits.getUnit(unitName);
        return value/toBeConverted.rate;
    }

    public static float CurrencyConversion(string unitName, float value) {
        Unit toBeConverted = Currency.getUnit(unitName);
        return (float) value/toBeConverted.rate;
    }

}