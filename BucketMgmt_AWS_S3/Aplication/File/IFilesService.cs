using Amazon.S3.Model;

namespace BucketMgmt_AWS_S3.Aplication.File;

public interface IFilesService
{
    Task Upload(IFormFile file, string bucketName);
    Task<IEnumerable<string>> ListFiles(string bucketName);
    Task DeleteFile(string fileName, string bucketName);
    string DownloadFile(string fileName, string bucketName);
}
