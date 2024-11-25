using FileUploadServiceApi.Application.Commands;
using FileUploadServiceApi.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadServiceApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            var result = await _mediator.Send(new UploadFileCommand { File = file });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFileById(Guid id)
        {
            var result = await _mediator.Send(new GetFileByIdQuery { Id = id });
            return Ok(result);
        }
    }
}
