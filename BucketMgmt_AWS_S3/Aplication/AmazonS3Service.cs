using BucketMgmt_AWS_S3.Infra;

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
        await _awsClient.UploadFileAsync(file, new Guid().ToString(), bucketName);
    }

    public void Delete() { }

    public void Read() { }
}
