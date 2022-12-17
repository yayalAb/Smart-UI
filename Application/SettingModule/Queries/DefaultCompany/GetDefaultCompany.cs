
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.SettingModule.Command.UpdateSettingCommand;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SettingModule.Queries.DefaultCompany;

public record GetDefaultCompany : IRequest<SettingDto> { }

public class GetDefaultCompanyHandler : IRequestHandler<GetDefaultCompany, SettingDto>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetDefaultCompanyHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SettingDto> Handle(GetDefaultCompany request, CancellationToken cancellationToken)
    {

        var setting = await _context.Settings
            .Include(s => s.DefaultCompany)
            .Include(s => s.DefaultCompany.Address)
            .Include(s => s.DefaultCompany.BankInformation)
            .Select(s => new Setting
            {
                Id = s.Id,
                Email = s.Email,
                Password = s.Password,
                Port = s.Port,
                Host = s.Host,
                Protocol = s.Protocol,
                Username = s.Username,
                CompanyId = s.CompanyId,
                DefaultCompany = new Company
                {
                    Id = s.DefaultCompany.Id,
                    Name = s.DefaultCompany.Name,
                    TinNumber = s.DefaultCompany.Name,
                    CodeNIF = s.DefaultCompany.Name,

                    AddressId = s.DefaultCompany.AddressId,
                    Address = new Address
                    {
                        Id = s.DefaultCompany.Address.Id,
                        Email = s.DefaultCompany.Address.Email,
                        Phone = s.DefaultCompany.Address.Phone,
                        Region = s.DefaultCompany.Address.Region,
                        City = s.DefaultCompany.Address.City,
                        Subcity = s.DefaultCompany.Address.Subcity,
                        Country = s.DefaultCompany.Address.Country,
                        POBOX = s.DefaultCompany.Address.POBOX
                    },
                    BankInformation = s.DefaultCompany.BankInformation.Select(b => new BankInformation
                    {
                        Id = b.Id,
                        AccountHolderName = b.AccountHolderName,
                        BankName = b.BankName,
                        AccountNumber = b.AccountNumber,
                        SwiftCode = b.SwiftCode,
                        BankAddress = b.BankAddress,
                        CompanyId = b.CompanyId
                    }).ToList()
                },
            }).FirstOrDefaultAsync();

        if (setting == null)
        {
            throw new GhionException(CustomResponse.NotFound("Operation not found!"));
        }

        SettingDto ReturnSetting = _mapper.Map<SettingDto>(setting);
        ReturnSetting.Address = _mapper.Map<AddressUpdateDto>(setting.DefaultCompany.Address);
        ReturnSetting.BankInformation = _mapper.Map<BankInformationUpdateDto>(setting.DefaultCompany.BankInformation.First());

        return ReturnSetting;

    }
}
