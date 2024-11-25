using FileUploadServiceApi.Application.DTOs;
using MediatR;

namespace FileUploadServiceApi.Application.Commands
{
    public class UploadFileCommand : IRequest<FileDto>
    {
        public IFormFile? File { get; set; }
    }
}
