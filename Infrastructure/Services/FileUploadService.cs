

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace Infrastructure.Services
{
    public  class FileUploadService : IFileUploadService
    {
        private readonly AppDbContext _context;
        //to restrict file extensions
        public static readonly List<string> ImageExtensions = new() { ".JPG", ".BMP", ".PNG" }; 
        public static readonly List<string> DocumentExtensions = new() { ".PDF", ".CSV", ".DOC", ".DOCX" };
       


        public FileUploadService(AppDbContext context)
        {
          _context = context;
        }
        public async Task<(Result, int )> uploadFile(IFormFile file, FileType fileType)
        {
            var extension = Path.GetExtension(file.FileName);//get file name
            switch (fileType)
            {
                case FileType.Image:
                   
                    if (!ImageExtensions.Contains(extension.ToUpperInvariant()))
                    {
                        return (Result.Failure(new string[] { "image format must be in JPG, BMP or PNG" }),0);

                    }
                    else
                    {
                       using var dataStream = new MemoryStream();
                        await file.CopyToAsync(dataStream);
                        byte[] imageBytes = dataStream.ToArray();
                        var imageData = new Image
                        {
                            ImageData = imageBytes
                        };
                        _context.Images.Add(imageData);
                        await _context.SaveChangesAsync();
                        return (Result.Success(), imageData.Id);
                    }
                 case FileType.EcdDocument:
                 case   FileType.BillOfLoadingDocument:
                    if (!DocumentExtensions.Contains(extension.ToUpperInvariant()))
                    {
                        return (Result.Failure(new string[] { "document format must be in PDF, CSV , DOC, or DOCX" }), 0);

                    }
                    else
                    {
                        using var dataStream = new MemoryStream();
                        await file.CopyToAsync(dataStream);
                        byte[] docBytes = dataStream.ToArray();
                        
                            var document = new Document
                            {
                                DocumentData = docBytes,
                                Type = fileType.ToString(),

                            };

                        _context.Documents.Add(document);
                        await _context.SaveChangesAsync();
                        return (Result.Success(), document.Id);
                    }
                 default:
                    return (Result.Failure(new string[] { "Unknown FileType " }), 0);

            }
           
        }

        public async Task<Result> updateFile(IFormFile file, FileType fileType , int fileId)
        {
            var extension = Path.GetExtension(file.FileName);//get file name
            switch (fileType)
            {
                case FileType.Image:

                    if (!ImageExtensions.Contains(extension.ToUpperInvariant()))
                    {
                        return Result.Failure(new string[] { "image format must be in JPG, BMP or PNG" });

                    }
                    else
                    {
                        using var dataStream = new MemoryStream();
                        await file.CopyToAsync(dataStream);
                        byte[] imageBytes = dataStream.ToArray();

                        var oldImage = await  _context.Images.FindAsync(fileId);
                        if(oldImage == null)
                        {
                            throw new NotFoundException("Image", new { Id = fileId });
                        }
                        oldImage.ImageData = imageBytes;
                        _context.Images.Update(oldImage);
                        await _context.SaveChangesAsync();

                        return Result.Success();
                    }
                case FileType.EcdDocument:
                case FileType.BillOfLoadingDocument:
                    if (!DocumentExtensions.Contains(extension.ToUpperInvariant()))
                    {
                        return Result.Failure(new string[] { "document format must be in PDF, CSV , DOC, or DOCX" });

                    }
                    else
                    {
                        using var dataStream = new MemoryStream();
                        await file.CopyToAsync(dataStream);
                        byte[] docBytes = dataStream.ToArray();

                        var oldDocument = await _context.Documents.FindAsync(fileId);

                        if(oldDocument == null)
                        {
                            throw new NotFoundException("Document", new { Id = fileId });
                        }
                        oldDocument.DocumentData = docBytes;    
                        _context.Documents.Update(oldDocument);
                        await _context.SaveChangesAsync();
                        return Result.Success();
                    }
                default:
                    return Result.Failure(new string[] { "Unknown FileType " });

            }

        }

    }
}
