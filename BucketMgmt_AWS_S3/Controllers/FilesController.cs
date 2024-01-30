using BucketMgmt_AWS_S3.Aplication.File;
using Microsoft.AspNetCore.Mvc;

namespace BucketMgmt_AWS_S3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IFilesService _awsService;

    public FilesController(IFilesService awsService)
    {
        _awsService = awsService;
        _awsService = awsService;
    }

    /// <summary>
    /// 📦 Retrieves a list of file names from the specified bucket.
    /// </summary>
    /// <param name="bucketName">The name of the bucket to retrieve files from (default: "awscsharpbuket").</param>
    /// <returns>
    /// A collection of file names in the specified bucket.
    /// </returns>
    /// <remarks>
    /// **Summary**<br/>
    /// 🚀 This endpoint is used to retrieve a list of file names from the specified bucket.
    /// <br/><br/>
    /// **Note**<br/>
    /// 🛑 Ensure that the provided bucket name is valid.
    /// </remarks>
    /// <response code="200">OK - The list of file names was retrieved successfully.</response>

    [HttpGet("list-all-bucket-files")]
    public async Task<IEnumerable<string>> ReadFiles(string bucketName = "awscsharpbuket")
    {
        return await _awsService.ListFiles(bucketName);
    }

    /// <summary>
    /// 📤 Uploads a file to the specified bucket.
    /// </summary>
    /// <param name="inputFile">The file to be uploaded.</param>
    /// <returns>
    /// 200 OK - The file was uploaded successfully.
    /// </returns>
    /// <remarks>
    /// **Summary**<br/>
    /// 🚀 This endpoint is used to upload a file to the specified bucket.
    /// <br/><br/>
    /// **Note**<br/>
    /// 🛑 Ensure that the provided file is valid.
    /// </remarks>
    /// <response code="200">OK - The file was uploaded successfully.</response>

    [HttpPost("upload-file")]
    public async Task<ActionResult> Upload([FromForm] FileModel inputFile)
    {
        await _awsService.Upload(inputFile.File, "awscsharpbuket");
        return Ok();
    }

    /// <summary>
    /// 📂 Retrieves URLs from a specific file in the specified bucket
    /// </summary>
    /// <param name="fileName">The name of the file to retrieve.</param>
    /// <param name="bucketName">The name of the bucket containing the file (default: "awscsharpbuket").</param>
    /// <returns>
    /// 200 OK - Information about the specified file was retrieved successfully.
    /// </returns>
    /// <remarks>
    /// **Summary**<br/>
    /// 🚀 This endpoint is used to retrieve information about a specific file from the specified bucket.
    /// <br/><br/>
    /// **Note**<br/>
    /// 🛑 Ensure that the provided file name and bucket name are valid.
    /// </remarks>
    /// <response code="200">OK - Information about the specified file was retrieved successfully.</response>

    [HttpGet("get-one-file")]
    public async Task<IActionResult> getUrls(string fileName, string bucketName = "awscsharpbuket")
    {
        return Ok(await _awsService.GetOneFile(fileName, bucketName));
    }

    /// <summary>
    /// 📂 Retrieves URLs for all files in the specified bucket.
    /// </summary>
    /// <param name="bucketName">The name of the bucket containing the files (default: "awscsharpbuket").</param>
    /// <returns>
    /// 200 OK - URLs for all files in the specified bucket were retrieved successfully.
    /// </returns>
    /// <remarks>
    /// **Summary**<br/>
    /// 🚀 This endpoint is used to retrieve URLs for all files in the specified bucket.
    /// <br/><br/>
    /// **Note**<br/>
    /// 🛑 Ensure that the provided bucket name is valid.
    /// </remarks>
    /// <response code="200">OK - URLs for all files in the specified bucket were retrieved successfully.</response>

    [HttpGet("get-all-files")]
    public async Task<IActionResult> getUrl(string bucketName = "awscsharpbuket")
    {
       return Ok(await _awsService.GetFilesUrl(bucketName));
    }

    /// <summary>
    /// 🗑️ Deletes a specific file from the specified bucket.
    /// </summary>
    /// <param name="fileName">The name of the file to delete.</param>
    /// <param name="bucketName">The name of the bucket containing the file (default: "awscsharpbuket").</param>
    /// <remarks>
    /// **Summary**<br/>
    /// 🚀 This endpoint is used to delete a specific file from the specified bucket.
    /// <br/><br/>
    /// **Note**<br/>
    /// 🛑 Ensure that the provided file name and bucket name are valid.
    /// </remarks>
    /// <response code="204">No Content - The file was deleted successfully.</response>

    [HttpDelete("delete-file")]
    public async Task Delete(string fileName, string bucketName = "awscsharpbuket")
    {
        await _awsService.DeleteFile(fileName, bucketName);
    }
}
