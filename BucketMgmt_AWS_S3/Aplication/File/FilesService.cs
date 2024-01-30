using Amazon.S3.Model;
using S3ManagementAPI.Domain;
using System.Runtime.CompilerServices;

namespace BucketMgmt_AWS_S3.Aplication.File;

public class FilesService : IFilesService
{
    private readonly IAwsClient _awsClient;

    public FilesService(IAwsClient awsClient)
    {
        _awsClient = awsClient;
    }

    public async Task Upload(IFormFile file, string bucketName)
    {
        var fileType = file.GetType();
        var extension = file.FileName.ToLower();

        await _awsClient.UploadFileAsync(file, Guid.NewGuid().ToString(), bucketName);
    }

    public async Task<IEnumerable<string>> ListFiles(string bucketName) => await _awsClient.ListAllFilesFromBucket(bucketName);
    

    public async Task DeleteFile(string fileName, string bucketName)
    {
        await _awsClient.DeleteFileFromBucket(fileName, bucketName);
    }
    public async Task<IEnumerable<string>> GetFilesUrl(string bucketName) => await _awsClient.GetFilesUrl(bucketName);
    public async Task<IEnumerable<string>> GetOneFile(string fileName, string bucketName) => await _awsClient.GetFilesUrl(bucketName, fileName);
}
