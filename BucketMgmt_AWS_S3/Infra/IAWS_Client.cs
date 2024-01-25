namespace BucketMgmt_AWS_S3.Infra;

public interface IAWS_Client
{
    Task<bool> UploadFileAsync(IFormFile file, string fileName, string bucketName);
}
