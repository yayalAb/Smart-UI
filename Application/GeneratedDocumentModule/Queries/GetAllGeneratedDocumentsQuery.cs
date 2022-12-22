using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Service;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.GeneratedDocumentModule.Queries;
public record GetAllGeneratedDocumentsQuery : IRequest<List<GeneratedDocument>>
{
    public int OperationId { get; set; }
    public string documentType { get; set; } 
}
public class GetAllGeneratedDocumentsQueryHandler : IRequestHandler<GetAllGeneratedDocumentsQuery, List<GeneratedDocument>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllGeneratedDocumentsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<GeneratedDocument>> Handle(GetAllGeneratedDocumentsQuery request, CancellationToken cancellationToken)
    {
        return await _context.GeneratedDocuments
                        .Where(gd => gd.OperationId == request.OperationId && gd.DocumentType.ToUpper() == request.documentType.ToUpper())
                 .ToListAsync();
    }
}
