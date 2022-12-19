

using Application.Common.Interfaces;
using FluentValidation;


namespace Application.LookUp.Commands.UpdateLookup
{
    public class UpdateLookupCommandValidator : AbstractValidator<UpdateLookupCommand>
    {
        private readonly IAppDbContext _context;
        public UpdateLookupCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(l => l.Id)
                .NotEmpty()
                .NotNull();
            RuleFor(l => l.Key)
                .NotEmpty()
                .NotNull()
                .MaximumLength(45)
                .WithMessage("key is not in the correct format!");
            RuleFor(l => l.Value)
                .NotNull()
                .NotEmpty()
                // .Must(BeUnique)
                .WithMessage("lookup value is not in the correct format");
        }
        private bool BeUnique(UpdateLookupCommand lookup, string name)
        {
            return !_context.Lookups.Where(l => l.Value == name && lookup.Key == l.Key).Any();
        }

    }
}
