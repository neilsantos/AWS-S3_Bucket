using Amazon.S3.Model;
using BucketMgmt_AWS_S3.Aplication.File;
using Microsoft.AspNetCore.Mvc;

namespace BucketMgmt_AWS_S3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFilesService _awsService;

        public FilesController(IFilesService awsService)
        {
            _awsService = awsService;
        }

        [HttpGet("list-all-bucket-files")]
        public async Task<IEnumerable<string>> ReadFiles(string bucketName = "awscsharpbuket")
        {
            return await _awsService.ListFiles(bucketName);
        }

        [HttpPost("upload-file")]
        public async Task<ActionResult> Upload([FromForm] FileModel inputFile)
        {
            await _awsService.Upload(inputFile.File, "awscsharpbuket");
            return Ok();
        }

        [HttpGet("download-file")]
        public IActionResult Download(string fileName, string bucketName = "awscsharpbuket")
        {
            return Ok(_awsService.DownloadFile(fileName, bucketName));
        }

        [HttpDelete("delete-file")]
        public async Task Delete(string fileName, string bucketName = "awscsharpbuket")
        {
            await _awsService.DeleteFile(fileName, bucketName);
        }
    }
}
