using BucketMgmt_AWS_S3.Aplication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BucketMgmt_AWS_S3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class S3Management : ControllerBase
    {
        [HttpPost("FileUpload")]
        public async Task<ActionResult> Upload([FromServices] IAmazonS3Service awsService, [FromForm] FileViewModel inputFile)
        {
            await awsService.Upload(inputFile.File, "awscsharpbuket");
            return Ok();
        }

        [HttpGet("FileList")]
        public ActionResult Read()
        {
            return Ok();
        }

        [HttpDelete("DeleteFile")]
        public ActionResult Delete(string filename)
        {
            return Ok();
        }
    }
}
