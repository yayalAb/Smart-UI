using Application.Common.Interfaces;
using FluentValidation;

namespace Application.GoodModule.Commands.AssignGoodsCommand;
public class AssignGoodsCommandValidator : AbstractValidator<AssignGoodsCommand>{
    private readonly IAppDbContext _context;

    public AssignGoodsCommandValidator(IAppDbContext context)
    {
        _context = context;
        RuleFor(ag => ag.OperationId)
            .NotNull()
            .NotEmpty()
            .Must(BeFoundInDb).WithMessage("operation with the provided id is not found");
        RuleFor(ag => ag.Goods)
            .NotNull()
            .NotEmpty();
        RuleFor(ag => ag.Goods!.Select(g => g.Description))
            .NotNull()
            .NotEmpty();
        RuleFor(ag => ag.Goods!.Select(g => g.Weight))
            .NotNull()
            .NotEmpty();
        RuleFor(ag => ag.Goods!.Select(g => g.NumberOfPackages))
            .NotNull()
            .NotEmpty();
        When(ag => ag.Containers != null, () => {
            RuleFor(ag => ag.Containers!.Select(c =>c.SealNumber))
                .NotNull()
                .NotEmpty();
            RuleFor(ag => ag.Containers!.Select(c =>c.ContianerNumber))
                .NotNull()
                .NotEmpty();
            RuleFor(ag => ag.Containers!.Select(c =>c.Location))
                .NotNull()
                .NotEmpty();
        });
    
    }
    private bool BeFoundInDb(int operationId)
        {
            return  _context.Operations.Find(operationId) != null;
        }
}