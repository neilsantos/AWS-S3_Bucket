using FluentResults;

namespace BucketMgmt_AWS_S3.Aplication.Bucket;

public interface IBucketService
{
    Task<IEnumerable<string>> List();
    Task<Result> Create(string bucketName);
    Task<Result> Delete(string bucketName);
}
