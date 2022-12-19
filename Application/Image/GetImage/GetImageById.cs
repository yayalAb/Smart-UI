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

    public async Task</*HttpResponseMessage*/ string> Handle(GetImageById request, CancellationToken cancellationToken)
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

        if (request.Type.ToLower() == "truck")
        {
            var temp = await _context.Trucks.FindAsync(request.Id);
            if (temp == null)
            {
                throw new GhionException(CustomResponse.NotFound("driver not found"));
            }
            data = temp.Image;
        }

        //cannot find image on user

        // if(request.Type.ToLower() == "user"){
        //     var temp = _identity.AllUsers().Where(u => u.Id == request.Id.ToString()).FirstOrDefault();
        //     if(temp == null){
        //         throw new GhionException(CustomResponse.NotFound("driver not found"));
        //     }
        //     data = temp.;
        // }

        // HttpResponseMessage response = new HttpResponseMessage();
        // byte[] bytes = System.Convert.FromBase64String(data);

        // response.Content = new ByteArrayContent(bytes);
        // response.Content.LoadIntoBufferAsync(bytes.Length).Wait();
        // response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
        // return response;

        return data;

        // return File(new Byte[2], "image/jpeg");

    }
}