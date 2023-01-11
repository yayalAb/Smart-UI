using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.OperationModule.Queries.oprationDetail
{
    public class oprationDetail: IRequest<oprationDetailDto>
{
    public int Id { get; init; }
}
public class oprationDetailQueryHandler : IRequestHandler<oprationDetail, oprationDetailDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public oprationDetailQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<oprationDetailDto> Handle(oprationDetail request, CancellationToken cancellationToken)
    {
        var operation = _context.Operations
        
            .ProjectTo<oprationDetailDto>(_mapper.ConfigurationProvider)
            .FirstOrDefault(s => s.Id == request.Id);

        if (operation == null)
        {
            throw new GhionException(CustomResponse.NotFound("Operation not found"));
        }
        return operation;
    }
}
}
