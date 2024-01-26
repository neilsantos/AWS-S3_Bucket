using BucketMgmt_AWS_S3.Aplication;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BucketMgmt_AWS_S3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class S3ManagementController : ControllerBase
    {
        private readonly IAmazonS3Service _awsService;

        public S3ManagementController(IAmazonS3Service awsService)
        {
            _awsService = awsService;
        }

        [HttpGet("list-all-buckets")]
        public async Task<IEnumerable<string>> Read()
        {
            return await _awsService.ListBukets();
        }

        [HttpPost("file-upload")]
        public async Task<ActionResult> Upload([FromForm] FileViewModel inputFile)
        {
            await _awsService.Upload(inputFile.File, "awscsharpbuket");
            return Ok();
        }

        [HttpGet("list-all-bucket-files")]
        public async Task<IEnumerable<string>> ReadFiles(string bucketName)
        {
            return await _awsService.ListBuketFiles(bucketName);
        }

        [HttpGet("download-file")]
        public async Task Download(string fileName, string bucketName)
        {
            await _awsService.DownloadFile(fileName, bucketName);
        }

        [HttpDelete("delete-file")]
        public async Task Delete(string fileName, string bucketName)
        {
            await _awsService.DeleteFileFromBucket(fileName, bucketName);
        }
    }
}
