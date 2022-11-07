

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
                .NotNull();
            RuleFor(l => l.Name)
                .NotNull()
                .NotEmpty()
                .Must(BeUnique).WithMessage("lookup name must be unique");

        }
        private bool BeUnique(UpdateLookupCommand lookup, string name)
        {
            return !_context.Lookups.Where(l => l.Name == name && lookup.Type == l.Type).Any();
        }

    }
}
