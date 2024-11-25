using FileUploadServiceApi.Application.DTOs;
using MediatR;

namespace FileUploadServiceApi.Application.Queries
{
    public class GetFileByIdQuery : IRequest<FileDto>
    {
        public Guid Id { get; set; }
    }

}
