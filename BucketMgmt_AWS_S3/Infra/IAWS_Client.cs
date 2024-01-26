namespace BucketMgmt_AWS_S3.Infra;

public interface IAWS_Client
{
    Task UploadFileAsync(IFormFile file, string fileName, string bucketName);
    Task<IEnumerable<string>> ListAllBuckets();
    Task<IEnumerable<string>> ListAllFilesFromBucket(string bucketName);
    Task DeleteFileFromBucket(string fileName, string bucketName);
    Task DownloadFile(string bucketName, string fileName);
}
