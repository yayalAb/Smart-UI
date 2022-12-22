using Application.Common.Interfaces;
using Domain.Common.Units;
using FluentValidation;

namespace Application.SettingModule.Command.CreateTodayCurrencyRate;

public class AddCurrencyRateValidator : AbstractValidator<AddCurrencyRate>
{
    private readonly IAppDbContext _context;

    public AddCurrencyRateValidator(IAppDbContext context)
    {

        _context = context;

        RuleFor(u => u.Currency)
            .NotNull()
            .NotEmpty()
            .Must(BeofType)
            .WithMessage("Invalid Currency!");
        RuleFor(u => u.Rate)
            .NotNull()
            .NotEmpty()
            .WithMessage("Rate is required");

    }

    public bool BeofType(string currencyName){
        return Currency.Currencies.Contains(currencyName);
    }

}