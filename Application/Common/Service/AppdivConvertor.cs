
using Application.Common.Interfaces;
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
    public static Double WeightConversion(Unit toBeConverted, Double value) {
        return value/toBeConverted.rate;
    }
}