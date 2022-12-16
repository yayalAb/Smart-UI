
using Application.Common.Interfaces;
using Application.ContactPersonModule.Commands.ContactPersonUpdateCommand;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.CompanyModule.Commands.UpdateCompanyCommand
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateCompanyCommandValidator> logger;

        public UpdateCompanyCommandValidator(IAppDbContext context, ILogger<UpdateCompanyCommandValidator> logger)
        {
            _context = context;
            this.logger = logger;
            RuleFor(u => u.Name)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty()
                .WithMessage("name is not in the correct format!");
            RuleFor(u => u.TinNumber)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty()
                .WithMessage("tin number is not in the correct format!");
            RuleFor(u => u.CodeNIF)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty()
                .WithMessage("codenif is not in the correct format!");
            RuleFor(u => u.Address)
                .NotNull();
            RuleFor(u => u.Address.Id)
                .Must(BeFoundInAddresses)
                .WithMessage("Address with the provided id is not found");
            RuleFor(u => u.Address.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("invalid email address");
            RuleFor(u => u.ContactPeople)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.ContactPeople!.Select(cp => cp.Id))
                .Must(BeUnique).WithMessage("all contact people must have unique id ")
                .Must(BeFoundInContactPeople).WithMessage("one or more contact person with the entered id is not found");
            RuleFor(u => u.ContactPeople!.Select(cp => cp.Name))
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.ContactPeople!.Select(cp => cp.Email))
                .ForEach(em => em.EmailAddress().WithMessage("invalid contact person email address"));

            RuleFor(u => u.ContactPeople!.Select(cp => cp.Phone))
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.ContactPeople!.Select(cp => cp.TinNumber))
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.BankInformation)
                .NotNull()
                .NotEmpty();

            RuleFor(u => u.BankInformation.Select(bi => bi.Id))
                .Must(BeUnique).WithMessage("all bank information must have unique id ")
                .Must(BeFoundInBankInfo).WithMessage("one or more bank information with the entered id is not found");
            RuleFor(u => u.BankInformation.Select(bi => bi.AccountHolderName))
                .NotNull();
            RuleFor(u => u.BankInformation.Select(bi => bi.AccountNumber))
                .NotNull();
            RuleFor(u => u.BankInformation.Select(bi => bi.BankAddress))
                .NotNull();
            RuleFor(u => u.BankInformation.Select(bi => bi.BankName))
                .NotNull();
            RuleFor(u => u.BankInformation.Select(bi => bi.SwiftCode))
                .NotNull();
            RuleFor(u => u.BankInformation.Select(bi => bi.CurrencyType))
                .NotNull();
        }

        private bool BeFoundInAddresses(int? addressId)
        {
            return addressId == null || _context.Addresses.Where(a => a.Id == addressId).Any();
        }

        private bool BeFoundInContactPeople(IEnumerable<int?> contactPersonIds)
        {
            for (int i = 0; i < contactPersonIds.Count(); i++)
            {
                if (contactPersonIds.ToList()[i] != null && !_context.ContactPeople.Where(cp => cp.Id == contactPersonIds.ToList()[i]).Any())
                {
                    return false;
                }
            }
            return true;
        }
        private bool BeFoundInBankInfo(IEnumerable<int?> bankInfoIds)
        {
            for (int i = 0; i < bankInfoIds.Count(); i++)
            {
                if (bankInfoIds.ToList()[i] != null && !_context.BankInformation.Where(bi => bi.Id == bankInfoIds.ToList()[i]).Any())
                {
                    return false;
                }
            }
            return true;
        }

        private bool BeUnique(IEnumerable<int?> ids)
        {
            ids = ids.ToList().Where(id => id != null);
            return ids.ToList().Distinct().Count() == ids.ToList().Count();

        }

    }

}