using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Service;
using Application.CompanyModule.Queries;
using Application.GoodModule.Queries;
using Application.PortModule;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.GeneratedDocumentModule.Queries;
public record GetAllGeneratedDocumentsQuery : IRequest<List<FetchGeneratedDocumentDto>>
{
    public int OperationId { get; set; }
    public string documentType { get; set; }
}
public class GetAllGeneratedDocumentsQueryHandler : IRequestHandler<GetAllGeneratedDocumentsQuery, List<FetchGeneratedDocumentDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllGeneratedDocumentsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<FetchGeneratedDocumentDto>> Handle(GetAllGeneratedDocumentsQuery request, CancellationToken cancellationToken)
    {
        return await _context.GeneratedDocuments
                        .Where(gd => gd.OperationId == request.OperationId
                            && gd.DocumentType.ToUpper() == request.documentType.ToUpper())
                            .ProjectTo<FetchGeneratedDocumentDto>(_mapper.ConfigurationProvider)
                 .ToListAsync();
    }
}
