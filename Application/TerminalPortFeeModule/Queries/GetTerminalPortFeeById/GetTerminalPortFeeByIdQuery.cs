

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;


namespace Application.TerminalPortFeeModule.Queries.GetTerminalPortFeeById
{
    public record GetTerminalPortFeeByIdQuery : IRequest<TerminalPortFeeDto>
    {
        public int  Id { get; set; }   
    }
    public class GetTerminalPortFeeByIdQueryHandler : IRequestHandler<GetTerminalPortFeeByIdQuery, TerminalPortFeeDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetTerminalPortFeeByIdQueryHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TerminalPortFeeDto> Handle(GetTerminalPortFeeByIdQuery request, CancellationToken cancellationToken)
        {
            var terminalPortFee = _context.TerminalPortFees
              .ProjectTo<TerminalPortFeeDto>(_mapper.ConfigurationProvider)
              .FirstOrDefault(s => s.Id == request.Id);

            if (terminalPortFee == null)
            {
                throw new NotFoundException("Terminal port fee", new { request.Id });
            }
            return terminalPortFee;
        }
    }
}

