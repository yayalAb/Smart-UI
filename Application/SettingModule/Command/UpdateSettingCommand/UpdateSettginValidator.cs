using Application.Common.Interfaces;
using Application.User.Commands.AuthenticateUser;
using Domain.Enums;
using FluentValidation;

namespace Application.SettingModule.Command.UpdateSettingCommand;

public class UpdateSettingValidator : AbstractValidator<UpdateSetting> {
    private readonly IAppDbContext _context;

    public UpdateSettingValidator(IAppDbContext context)
    {

        _context = context;

        RuleFor(u => u.Host)
            .NotNull()
            .NotEmpty()
            .WithMessage("Host is not in the correct format!");
        RuleFor(u => u.Protocol)
            .NotNull()
            .MaximumLength(10)
            .NotEmpty()
            .WithMessage("Protocol is not in the correct format!");
        RuleFor(u => u.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .WithMessage("Email is not in the correct format!");
        RuleFor(u => u.Port)
            .NotNull()
            .NotEmpty()
            .MaximumLength(10)
            .WithMessage("port is not in the correct format!");
        RuleFor(u => u.Password)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(6)
            .WithMessage("password is not in the correct format! it should not be greater than 100 characters and less than 6 characters!");

    }

}