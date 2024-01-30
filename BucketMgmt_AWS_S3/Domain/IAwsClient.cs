using Amazon.S3.Model;

namespace S3ManagementAPI.Domain;

public interface IAwsClient
{
    Task UploadFileAsync(IFormFile file, string fileName, string bucketName);
    Task<IEnumerable<string>> ListAllBuckets();
    Task<IEnumerable<string>> ListAllFilesFromBucket(string bucketName);
    Task DeleteFileFromBucket(string fileName, string bucketName);
    Task<IEnumerable<string>> GetFilesUrl(string bucketName, string fileName="");
    Task<bool> DoesThisBucketExist(string bucketName);

}
