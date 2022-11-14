

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
<<<<<<< HEAD
            RuleFor(l => l.Type)
                .NotEmpty()
                .NotNull()
                .MaximumLength(45)
                .WithMessage("type is not in the correct format!");
            RuleFor(l => l.Name)
=======

            RuleFor(l => l.Key)
                .NotEmpty()
                .NotNull();
            RuleFor(l => l.Value)
>>>>>>> 68a589be846fc74a04d36d873b4bfefc93d8539c
                .NotNull()
                .NotEmpty()
                // .Must(BeUnique)
                .WithMessage("lookup name is not in the correct format");
        }
        private bool BeUnique(UpdateLookupCommand lookup, string name)
        {
            return !_context.Lookups.Where(l => l.Value == name && lookup.Key == l.Key).Any();
        }

    }
}
