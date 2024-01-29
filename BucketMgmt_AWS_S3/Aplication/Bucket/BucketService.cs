using FluentResults;
using S3ManagementAPI.Domain;

namespace BucketMgmt_AWS_S3.Aplication.Bucket;

public class BucketService : IBucketService
{
    private readonly IAwsClient _awsClient;
    public BucketService(IAwsClient awsClient)
    {
        _awsClient = awsClient;
    }

    public async Task<IEnumerable<string>> List() => await _awsClient.ListAllBuckets();

    public async Task<Result> Create(string bucketName)
    {
        var doesExist = await _awsClient.DoesThisBucketExist(bucketName);

        if (doesExist)
            return Result.Fail("This bucket already exists");

        return Result.Ok();
    }

    public async Task<Result> Delete(string bucketName)
    {
        var doesExist = await _awsClient.DoesThisBucketExist(bucketName);

        if (!doesExist)
            return Result.Fail("This bucket doesn't exist");

        return Result.Ok();
    }
}
