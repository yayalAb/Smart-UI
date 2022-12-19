using Application.Common.Interfaces;
using FluentValidation;

namespace Application.GoodModule.Commands.UpdateGoodCommand
{
    public class UpdateGoodValidator : AbstractValidator<UpdateGoodCommand>
    {
        private readonly IAppDbContext _context;
        public UpdateGoodValidator(IAppDbContext context)
        {
            _context = context;
            RuleFor(ag => ag.OperationId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInDb).WithMessage("operation with the provided id is not found");
            When(ag => ag.Goods != null, () =>
            {
                RuleFor(ag => ag.Goods!.Select(g => g.LocationPortId))
                    .Must(ag => BeFoundInDbList(ag, "port")).WithMessage("one or more location port of a good with the provided id is not found ");

                RuleFor(ag => ag.Goods!.Select(g => g.Id))
                    .Must(g => BeFoundInDbList(g, "good")).WithMessage("one or more good with the provided id is not found ");

                RuleFor(ag => ag.Goods!.Select(g => g.Description))
                    .NotNull()
                    .NotEmpty();
                RuleFor(ag => ag.Goods!.Select(g => g.Type))
                    .NotNull()
                    .NotEmpty();
                RuleFor(ag => ag.Goods!.Select(g => g.Weight))
                    .NotNull()
                    .NotEmpty();
            });

            When(ag => ag.Containers != null, () =>
            {
                RuleFor(ag => ag.Containers!.Select(c => c.Id))
                    .Must(c => BeFoundInDbList(c, "container")).WithMessage("one or more good with the provided id is not found "); ;
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
                .Must(ag => BeFoundInDbList(ag, "port")).WithMessage($"one or more location port of a container with the provided id is not found ");
                //validation for goods inside containers 

                RuleFor(ag => ag.Containers!.SelectMany(c => c.Goods!.Select(g => g.LocationPortId)))
                    .Must(ag => BeFoundInDbList(ag, "port")).WithMessage("one or more location port of a good with the provided id is not found ");

                RuleFor(ag => ag.Containers!.SelectMany(c => c.Goods!.Select(g => g.Id)))
                     .Must(g => BeFoundInDbList(g, "good")).WithMessage("one or more good with the provided id is not found ");

                RuleFor(ag => ag.Containers!.SelectMany(c => c.Goods!.Select(g => g.Description)))
                     .NotNull()
                     .NotEmpty();
                RuleFor(ag => ag.Containers!.SelectMany(c => c.Goods!.Select(g => g.Weight)))
                     .NotNull()
                     .NotEmpty();
                RuleFor(ag => ag.Containers!.SelectMany(c => c.Goods!.Select(g => g.Type)))
                     .NotNull()
                     .NotEmpty();

            });

        }

        private bool BeFoundInDb(int operationId)
        {
            return _context.Operations.Find(operationId) != null;
        }
        private bool BeFoundInDbList(IEnumerable<int?> ids, string table)
        {

            if (ids == null)
            {
                return true;
            }

            for (int i = 0; i < ids.ToList().Count; i++)
            {
                if (ids.ToList()[i] == null)
                {
                    return true;
                }
                switch (table)
                {
                    case "port":
                        if (_context.Ports.Find(ids.ToList()[i]) == null)
                        {
                            return false;
                        }
                        break;
                    case "container":
                        if (_context.Containers.Find(ids.ToList()[i]) == null)
                        {
                            return false;
                        }
                        break;
                    case "good":
                        if (_context.Goods.Find(ids.ToList()[i]) == null)
                        {
                            return false;
                        }
                        break;
                }


            }
            return true;
        }
    }
}