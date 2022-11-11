using Application.Common.Interfaces;
using FluentValidation;

namespace Application.LookUp.Commands.CreateLookUpKey;

public class CreateLookUpKeyValidator : AbstractValidator<CreateLookUpKey>
{
    private readonly IAppDbContext _context;

    public CreateLookUpKeyValidator(IAppDbContext context)
    {
        _context = context; 

        RuleFor(l => l.Name)
            .NotEmpty()
            .NotNull()
            .Must(BeUnique);
        
    }
    private bool BeUnique(CreateLookUpKey lookup ,string name)
    {
        return !_context.Lookups.Where(l => l.Name == name && l.Type == "key" ).Any();
    }

}