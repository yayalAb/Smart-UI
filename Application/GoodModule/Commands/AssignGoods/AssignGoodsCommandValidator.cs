using Application.Common.Interfaces;
using Domain.Common.Units;
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
            RuleFor(ag => ag.Goods!.Select(g => g.Type))
                .NotNull()
                .NotEmpty();
            RuleFor(ag => ag.Goods!.Select(g => g.Weight))
                .NotNull()
                .NotEmpty();
            RuleFor(ag => ag.Goods!.Select(g => g.UnitPrice))
                .NotNull();
            RuleFor(ag => ag.Goods!.Select(g => g.Unit))
                .NotNull();
            // RuleFor(ag => ag.Goods!.Select(g => g.NumberOfPackages))
            //     .NotNull()
            //     .NotEmpty();
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

            RuleFor(ag => ag.Containers!.SelectMany(c => c.Goods!.Select(g => g.LocationPortId)))
                .Must(BeFoundInPortTable).WithMessage("one or more location port of a good with the provided id is not found ");


            RuleFor(ag => ag.Containers!.SelectMany(c => c.Goods!.Select(g => g.Description)))
                .NotNull()
                .NotEmpty();
            RuleFor(ag => ag.Containers!.SelectMany(c => c.Goods!.Select(g => g.Weight)))
                .NotNull()
                .NotEmpty();
            RuleFor(ag => ag.Containers!.SelectMany(c => c.Goods!.Select(g => g.Type)))
                .NotNull()
                .NotEmpty();
            // RuleFor(ag => ag.Containers!.SelectMany(c => c.Goods!.Select(g => g.NumberOfPackages)))
            //     .NotNull()
            //     .NotEmpty();
            RuleFor(ag => ag.Containers!.SelectMany(c => c.Goods!.Select(g => g.UnitPrice)))
                .NotNull();
            RuleFor(ag => ag.Containers!.SelectMany(c => c.Goods!.Select(g => g.Unit)))
                .NotNull()
                .Must(BeOfCurrencyType);
        });

    }

    private bool BeFoundInDb(int operationId)
    {
        return _context.Operations.Find(operationId) != null;
    }
    private bool BeFoundInPortTable(IEnumerable<int?> locationPortIds)
    {
        if (locationPortIds == null)
        {
            return true;
        }

        for (int i = 0; i < locationPortIds.ToList().Count; i++)
        {
            if (locationPortIds.ToList()[i] == null)
            {
                return true;
            }
            if (_context.Ports.Find(locationPortIds.ToList()[i]) == null)
            {
                return false;
            }

        }
        return true;
    }

    private bool BeOfCurrencyType(IEnumerable<string> units){

        foreach (var unit in units) {
            if(!Currency.Currencies.ToList().Contains(unit)){
                return false;
            }
        }

        return true;
        
    }

    private bool BeOfWeightUnitType(IEnumerable<string> units){
        
        foreach (var unit in units) {
            if(!WeightUnits.Units.ToList().Contains(unit)){
                return false;
            }
        }

        return true;
    }
}