using FileUploadServiceApi.Application.DTOs;
using FileUploadServiceApi.Application.Interfaces;
using FileUploadServiceApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;

namespace FileUploadServiceApi.Application.Commands
{
  
        public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, FileDto>
        {
            private readonly IFileRepository _fileRepository;
            private readonly IBlobStorageService _blobStorageService;

            public UploadFileCommandHandler(IFileRepository fileRepository, IBlobStorageService blobStorageService)
            {
                _fileRepository = fileRepository;
                _blobStorageService = blobStorageService;
            }

            public async Task<FileDto> Handle(UploadFileCommand request, CancellationToken cancellationToken)
            {
                // Save file to blob storage
                var filePath = await _blobStorageService.UploadFileAsync(request.File);

                // Save file info in the database
                var file = new Domain.Entities.File
                {
                    FileName = request.File.FileName,
                    FilePath = filePath,
                    FileType = request.File.ContentType,
                    FileSize = request.File.Length,
                    UploadedAt = DateTime.UtcNow
                };

                await _fileRepository.AddAsync(file);
                return new FileDto
                {
                    Id = file.Id,
                    FileName = file.FileName,
                    FilePath = file.FilePath,
                    FileType = file.FileType
                };
            }
        }    

}
