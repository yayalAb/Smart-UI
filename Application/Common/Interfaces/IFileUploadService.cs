
using Application.Common.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IFileUploadService
    {
        Task<(Result result, int Id)> uploadFile(IFormFile file, FileType fileType );
        Task<Result> updateFile(IFormFile file, FileType fileType , int fileId);
    }
}
