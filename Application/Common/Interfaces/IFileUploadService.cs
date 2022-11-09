
using Application.Common.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IFileUploadService
    {
        Task<(Result result, int imageId)> uploadFile(IFormFile file, FileType fileType ,  int operationId = 0);
        Task<Result> updateFile(IFormFile file, FileType fileType , int fileId);
    }
}
