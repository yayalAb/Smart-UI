

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
            RuleFor(l => l.Type)
                .NotEmpty()
                .NotNull()
                .MaximumLength(45)
                .WithMessage("type is not in the correct format!");
            RuleFor(l => l.Name)
                .NotNull()
                .NotEmpty()
                // .Must(BeUnique)
                .WithMessage("lookup name is not in the correct format");
        }
        private bool BeUnique(UpdateLookupCommand lookup, string name)
        {
            return !_context.Lookups.Where(l => l.Name == name && lookup.Type == l.Type).Any();
        }

    }
}
