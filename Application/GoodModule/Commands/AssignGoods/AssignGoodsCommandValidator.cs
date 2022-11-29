using Application.Common.Interfaces;
using FluentValidation;

namespace Application.GoodModule.Commands.AssignGoodsCommand;
public class AssignGoodsCommandValidator : AbstractValidator<AssignGoodsCommand>
{
    private readonly IAppDbContext _context;

    public AssignGoodsCommandValidator(IAppDbContext context)
    {
        _context = context;
        RuleFor(ag => ag.OperationId)
            .NotNull()
            .NotEmpty()
            .Must(BeFoundInDb).WithMessage("operation with the provided id is not found");
        When(ag => ag.Goods != null, () => 
        {
            RuleFor(ag => ag.Goods!.Select(g => g.LocationPortId))
                .Must(BeFoundInPortTable).WithMessage("one or more location port of a good with the provided id is not found ");


            RuleFor(ag => ag.Goods!.Select(g => g.Description))
                .NotNull()
                .NotEmpty();
            RuleFor(ag => ag.Goods!.Select(g => g.Weight))
                .NotNull()
                .NotEmpty();
            RuleFor(ag => ag.Goods!.Select(g => g.NumberOfPackages))
                .NotNull()
                .NotEmpty();
        });
       
        When(ag => ag.Containers != null, () =>
        {
            RuleFor(ag => ag.Containers!.Select(c => c.SealNumber))
                .NotNull()
                .NotEmpty();
            RuleFor(ag => ag.Containers!.Select(c => c.Size))
                .NotNull()
                .NotEmpty();
            RuleFor(ag => ag.Containers!.Select(c => c.ContianerNumber))
                .NotNull()
                .NotEmpty();
            RuleFor(ag => ag.Containers!.Select(c => c.Location))
                .NotNull()
                .NotEmpty();
            RuleFor(ag => ag.Containers!.Select(c => c.LocationPortId))
            .Must(BeFoundInPortTable).WithMessage($"one or more location port of a container with the provided id is not found ");
            
        });

    }

    private bool BeFoundInDb(int operationId)
    {
        return _context.Operations.Find(operationId) != null;
    }
    private bool BeFoundInPortTable(IEnumerable<int?> locationPortIds)
    {
        if(locationPortIds == null ){
            return true; 
        }

        for(int i = 0; i<locationPortIds.ToList().Count; i++){
            if(locationPortIds.ToList()[i] == null){
                return true;
            }
            if(_context.Ports.Find(locationPortIds.ToList()[i]) == null){
                return false; 
            }
           
        }
        return true; 
    }
}