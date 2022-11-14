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

            RuleFor(l => l.Type)
                .NotEmpty()
                .NotNull()
                .MaximumLength(45)
                .WithMessage("type is not in the correct format");
            RuleFor(l => l.Name)
                .NotNull()
                .NotEmpty()
                // .Must(BeUnique)
                .MinimumLength(45)
                .WithMessage("lookup name not in the correct format");
          
        }
        
        private bool BeUnique(CreateLookupCommand lookup ,string name)
        {
            return !_context.Lookups.Where(l=>l.Name == name && lookup.Type == l.Type ).Any();  
        }

    }
}
