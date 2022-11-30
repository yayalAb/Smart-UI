
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.OperationFollowupModule.Commands.UpdateStatus;

public class UpdateOperationCommandValidator : AbstractValidator<UpdateStatus>
{
    private readonly IAppDbContext _context;

    public UpdateOperationCommandValidator(IAppDbContext context)
    {
        _context = context;

        RuleFor(o => o.Id)
            .NotNull()
            .WithMessage("status id is requiered!");

    }
}