
using Application.Common.Interfaces;
using Application.OperationFollowupModule;
using AutoMapper;
using MediatR;

namespace Application.OperationDocuments.Queries.T1Document;

public record T1Document : IRequest<T1DocumentDto> {
    public int OperationId {get; init;}
}

public class T1DocumentHandler : IRequestHandler<T1Document, T1DocumentDto>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly OperationEventHandler _operationEvent;

    public T1DocumentHandler(IAppDbContext context, IMapper mapper, OperationEventHandler operationEvent) {
        _context = context;
        _mapper = mapper;
        _operationEvent = operationEvent;
    }

    public Task<T1DocumentDto> Handle(T1Document request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}