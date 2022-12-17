using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.OperationModule.Queries.GetOperationById;
public class GetOperationByIdQuery : IRequest<OperationDto>
{
    public int Id { get; init; }
}
public class GetOperationByIdQueryHandler : IRequestHandler<GetOperationByIdQuery, OperationDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetOperationByIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<OperationDto> Handle(GetOperationByIdQuery request, CancellationToken cancellationToken)
    {
        var operation = _context.Operations
            .ProjectTo<OperationDto>(_mapper.ConfigurationProvider)
            .FirstOrDefault(s => s.Id == request.Id);

        if (operation == null)
        {
            throw new GhionException(CustomResponse.NotFound("Operation not found"));
        }
        return operation;
    }
}
