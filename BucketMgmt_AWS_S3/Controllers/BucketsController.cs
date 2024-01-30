using Amazon.S3.Model;
using BucketMgmt_AWS_S3.Aplication.Bucket;
using BucketMgmt_AWS_S3.Aplication.File;
using FluentResults;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BucketMgmt_AWS_S3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BucketsController : ControllerBase
    {
        private readonly IBucketService _bucketService;

        public BucketsController(IBucketService bucketService)
        {
            _bucketService = bucketService;
        }

        /// <summary>
        /// 📋 Retrieves a list of all buckets.
        /// </summary>
        /// <returns>
        /// A collection of bucket names.
        /// </returns>
        /// <remarks>
        /// **Summary**<br/>
        /// 🚀 This endpoint is used to retrieve a list of all buckets.
        /// </remarks>
        /// <response code="200">OK - The list of buckets was retrieved successfully.</response>
        [HttpGet("list-buckets")]
        public async Task<IEnumerable<string>> Read()
        {
            return await _bucketService.List();
        }

        /// <summary>
        /// 📦 Creates a new bucket with the specified name.
        /// </summary>
        /// <param name="bucketName">The name of the bucket to be created.</param>
        /// <remarks>
        /// **Summary**<br/>
        /// ✨ This endpoint is used to create a new bucket with the provided name.
        /// <br/><br/>
        /// **Note**<br/>
        /// 🚨 Ensure that the provided bucket name is valid and follows any naming conventions.
        /// </remarks>
        /// <response code="204">No Content - The bucket was created successfully.</response>
        /// <response code="400">Bad Request - The creation failed due to invalid input or other client-side errors.</response>
        [HttpPost("create-bucket")]
        public async Task<IActionResult> Create(string bucketName)
        {
            var result = await _bucketService.Create(bucketName);
            
            if (result.IsFailed)
                return BadRequest(result.Errors.Select(x => x.Message));

            return NoContent();
        }

        /// <summary>
        /// 🗑️ Deletes a bucket with the specified name.
        /// </summary>
        /// <param name="bucketName">The name of the bucket to be deleted.</param>
        /// <remarks>
        /// **Summary**<br/>
        /// 🚫 This endpoint is used to delete a bucket identified by the provided name.
        /// <br/><br/>
        /// **Note**<br/>
        /// ❗ Ensure that the specified bucket name exists before attempting to delete it.
        /// </remarks>
        /// <response code="204">No Content - The bucket was deleted successfully.</response>
        /// <response code="404">Not Found - The specified bucket does not exist.</response>
        /// <response code="500">Internal Server Error - An unexpected error occurred during the deletion process.</response>

        [HttpDelete("delete-bucket")]
        public async Task Delete(string bucketName)
        {
            await _bucketService.Delete(bucketName);
        }

    }
}
