namespace BucketMgmt_AWS_S3.Aplication;

public interface IAmazonS3Service
{
    Task Upload(IFormFile file, string bucketName);
}
