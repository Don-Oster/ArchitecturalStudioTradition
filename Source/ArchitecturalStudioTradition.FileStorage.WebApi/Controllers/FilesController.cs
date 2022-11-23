using ArchitecturalStudioTradition.CQRS;
using ArchitecturalStudioTradition.FileStorage.Application.Files.GetFile;
using ArchitecturalStudioTradition.FileStorage.Application.Files.GetFileList;
using ArchitecturalStudioTradition.FileStorage.Application.Files.SaveFile;
using ArchitecturalStudioTradition.FileStorage.Contract.v1;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace ArchitecturalStudioTradition.FileStorage.WebApi.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        public FilesController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        [HttpGet]
        [Route("list")]
        [SwaggerResponse(200, typeof(GetFileListResponse), Description = "Gets list of files by hash", IsNullable = true)]
        public async Task<GetFileListResponse> GetList([FromQuery] string hash)
        {
            var query = new GetFileListQuery
            {
                Hash = hash
            };

            return await _commandBus.SendAsync(query);
        }

        [HttpGet]
        [SwaggerResponse(200, typeof(GetFileResponse), Description = "Gets file by hash", IsNullable = true)]
        public async Task<GetFileResponse> Get([FromQuery] string hash)
        {
            var query = new GetFileQuery
            {
                Hash = hash
            };

            return await _commandBus.SendAsync(query);
        }

        [HttpPost]
        [Route("save")]
        [SwaggerResponse(200, typeof(string), Description = "Saves provided file", IsNullable = false)]
        public async Task<string> Save([FromBody] SaveFileRequest request)
        {
            var command = new SaveFileCommand
            {
                FileName = request.FileName,
                FileContent = request.FileContent
            };

            return await _commandBus.SendAsync(command);
        }
    }
}