
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class FileUploadService : IFileUploadService
    {

        //to restrict file extensions
        public static readonly List<string> ImageExtensions = new() { ".JPG", ".BMP", ".PNG" };
        public static readonly List<string> DocumentExtensions = new() { ".PDF", ".CSV", ".DOC", ".DOCX" };



        public FileUploadService()
        {

        }
        public async Task<(Result, byte[]?)> GetFileByte(IFormFile file, FileType fileType)
        {
            var extension = Path.GetExtension(file.FileName);//get file name
            switch (fileType)
            {
                case FileType.Image:

                    if (!ImageExtensions.Contains(extension.ToUpperInvariant()))
                    {
                        return (Result.Failure(new string[] { "image format must be in JPG, BMP or PNG" }), null);

                    }
                    else
                    {
                        using var dataStream = new MemoryStream();
                        await file.CopyToAsync(dataStream);
                        byte[] imageBytes = dataStream.ToArray();

                        return (Result.Success(), imageBytes);
                    }
                case FileType.EcdDocument:
                case FileType.SourceDocument:
                    if (!DocumentExtensions.Contains(extension.ToUpperInvariant()))
                    {
                        return (Result.Failure(new string[] { "document format must be in PDF, CSV , DOC, or DOCX" }), null);

                    }
                    else
                    {
                        using var dataStream = new MemoryStream();
                        await file.CopyToAsync(dataStream);
                        byte[] docBytes = dataStream.ToArray();

                        return (Result.Success(), docBytes);
                    }
                default:
                    return (Result.Failure(new string[] { "Unknown FileType " }), null);

            }

        }


    }
}
