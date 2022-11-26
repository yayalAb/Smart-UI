
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.OperationFollowupModule.Commands.UpdateStatus;

public class UpdateOperationCommandValidator : AbstractValidator<UpdateStatus>
{
    private readonly IAppDbContext _context;

    public UpdateOperationCommandValidator(IAppDbContext context)
    {
        _context = context;

        RuleFor(o => o.GeneratedDocumentName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("Generated document name is requiered!");
        RuleFor(o => o.IsApproved)
            .NotNull()
            .WithMessage("approval is requiered!");

    }
}