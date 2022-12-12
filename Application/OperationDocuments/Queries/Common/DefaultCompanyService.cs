
using Application.Common.Interfaces;
using Application.SettingModule.Queries.DefaultCompany;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application.SettingModule.Command.UpdateSettingCommand;
using Application.Common.Models;
using Application.Common.Exceptions;

namespace Application.Common;

public class DefaultCompanyService {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public DefaultCompanyService(IAppDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<SettingDto> GetDefaultCompanyAsync() {

        var setting = await _context.Settings
            .Include(s => s.DefaultCompany)
            .Include(s => s.DefaultCompany.Address)
            .Include(s => s.DefaultCompany.BankInformation)
            .Select(s => new SettingDto {
                DefaultCompany = new CompanyUpdateDto {
                    Id = s.DefaultCompany.Id,
                    Name = s.DefaultCompany.Name,
                    TinNumber = s.DefaultCompany.Name,
                    CodeNIF = s.DefaultCompany.CodeNIF,
                    // ContactPersonId = s.DefaultCompany.ContactPersonId,
                    AddressId = s.DefaultCompany.AddressId
                },
                BankInformation = s.DefaultCompany.BankInformation.Select(b => new BankInformationUpdateDto {
                    Id = b.Id,
                    AccountHolderName = b.AccountHolderName,
                    BankName = b.BankName,
                    AccountNumber = b.AccountNumber,
                    SwiftCode = b.SwiftCode,
                    BankAddress = b.BankAddress,
                    CompanyId = b.CompanyId
                }).First(),
                Address = new AddressUpdateDto {
                    Id = s.DefaultCompany.Address.Id,
                    Email = s.DefaultCompany.Address.Email,
                    Phone = s.DefaultCompany.Address.Phone,
                    Region = s.DefaultCompany.Address.Region,
                    City = s.DefaultCompany.Address.City,
                    Subcity = s.DefaultCompany.Address.Subcity,
                    Country = s.DefaultCompany.Address.Country,
                    POBOX = s.DefaultCompany.Address.POBOX
                }
            }).FirstOrDefaultAsync();

        if(setting == null){
            throw new GhionException(CustomResponse.NotFound("Default Company setting  not found!"));
        }

        return setting;

    }

}