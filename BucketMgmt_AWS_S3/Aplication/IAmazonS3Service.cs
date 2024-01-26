namespace BucketMgmt_AWS_S3.Aplication;

public interface IAmazonS3Service
{
    Task Upload(IFormFile file, string bucketName);
    Task<IEnumerable<string>> ListBukets();
    Task<IEnumerable<string>> ListBuketFiles(string bucketName);
    Task DeleteFileFromBucket(string fileName, string bucketName);
    Task DownloadFile(string fileName, string bucketName);
}
