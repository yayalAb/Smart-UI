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
<<<<<<< HEAD
                .NotNull()
                .MaximumLength(45)
                .WithMessage("type is not in the correct format");
            RuleFor(l => l.Name)
=======
                .NotNull();
            RuleFor(l => l.Value)
>>>>>>> 68a589be846fc74a04d36d873b4bfefc93d8539c
                .NotNull()
                .NotEmpty()
                // .Must(BeUnique)
                .MinimumLength(45)
                .WithMessage("lookup name not in the correct format");
          
        }
        
        private bool BeUnique(CreateLookupCommand lookup ,string name)
        {
            return !_context.Lookups.Where(l=>l.Value == name && lookup.Key == l.Key ).Any();  
        }

    }
}
