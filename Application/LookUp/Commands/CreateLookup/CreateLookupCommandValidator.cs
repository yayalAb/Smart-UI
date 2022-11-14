using Application.Common.Interfaces;
using FluentValidation;

namespace Application.LookUp.Commands.CreateLookup
{
    public class CreateLookupCommandValidator : AbstractValidator<CreateLookupCommand>    
    {
        private readonly IAppDbContext _context;

        public CreateLookupCommandValidator(IAppDbContext context)
        {
            _context = context; 

            RuleFor(l => l.Key)
                .NotEmpty()
                .NotNull();
            RuleFor(l => l.Value)
                .NotNull()
                .NotEmpty()
                .Must(BeUnique).WithMessage("lookup name must be unique");
          
        }
        private bool BeUnique(CreateLookupCommand lookup ,string name)
        {
            return !_context.Lookups.Where(l=>l.Value == name && lookup.Key == l.Key ).Any();  
        }

    }
}
