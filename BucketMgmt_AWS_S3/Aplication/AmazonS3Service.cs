using BucketMgmt_AWS_S3.Infra;
using System.Runtime.CompilerServices;

namespace BucketMgmt_AWS_S3.Aplication;

public class AmazonS3Service : IAmazonS3Service
{
    private readonly IAWS_Client _awsClient;

    public AmazonS3Service(IAWS_Client awsClient)
    {
        _awsClient = awsClient;
    }

    public async Task Upload(IFormFile file, string bucketName)
    {
        await _awsClient.UploadFileAsync(file, Guid.NewGuid().ToString(), bucketName);
    }

    public async Task<IEnumerable<string>> ListBuketFiles(string bucketName) => await _awsClient.ListAllFilesFromBucket(bucketName);
    public async Task<IEnumerable<string>> ListBukets() => await _awsClient.ListAllBuckets();

    public async Task DeleteFileFromBucket(string fileName, string bucketName)
    {
        await _awsClient.DeleteFileFromBucket(fileName, bucketName);
    }
    public async Task DownloadFile(string fileName, string bucketName) => await _awsClient.DownloadFile(fileName, bucketName);

}
