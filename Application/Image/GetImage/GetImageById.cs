using System.Net.Http.Headers;
using System.Text;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Image.GetImage;

public record GetImageById : IRequest<string>
{
    public int Id { get; set; }
    public string Type { get; set; }
}

public class GetImageByIdHandler : IRequestHandler<GetImageById, string>
{

    private IAppDbContext _context { get; init; }
    private IIdentityService _identity { get; init; }

    public GetImageByIdHandler(IAppDbContext context, IIdentityService identity)
    {
        _identity = identity;
        _context = context;
    }

    public async Task<string> Handle(GetImageById request, CancellationToken cancellationToken)
    {

        string data = "";
        if (request.Type.ToLower() == "driver")
        {
            var temp = await _context.Drivers.FindAsync(request.Id);
            if (temp == null)
            {
                throw new GhionException(CustomResponse.NotFound("driver not found"));
            }
            data = temp.Image;
        }

        else if (request.Type.ToLower() == "truck")
        {
            var temp = await _context.Trucks.FindAsync(request.Id);
            if (temp == null)
            {
                throw new GhionException(CustomResponse.NotFound("truck not found"));
            }
            data = temp.Image;
        }
        else if (request.Type.ToLower() == "shippingagent")
        {
            var temp = await _context.ShippingAgents.FindAsync(request.Id);
            if (temp == null)
            {
                throw new GhionException(CustomResponse.NotFound("shipping agent not found"));
            }
            data = temp.Image;
        }
        else{
            throw new GhionException(CustomResponse.BadRequest("invalid type"));
        }
      
        return data;
    }
}