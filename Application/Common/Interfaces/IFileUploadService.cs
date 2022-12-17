
using Application.Common.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IFileUploadService
    {
        Task<(Result result, byte[]? byteData)> GetFileByte(IFormFile file, FileType fileType);
    }
}
