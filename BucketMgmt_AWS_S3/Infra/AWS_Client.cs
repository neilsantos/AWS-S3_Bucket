using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace BucketMgmt_AWS_S3.Infra
{
    public class AWS_Client : IAWS_Client
    {
        private readonly IAmazonS3 _awsClient;
        private readonly string _storageClass;

        public AWS_Client(IConfiguration connections)
        {
            string accessKeyId = connections.GetSection("AwsCredentials").GetValue<string>("AccessKeyId")!;
            string secretAccessKey = connections.GetSection("AwsCredentials").GetValue<string>("SecretAccessKey")!;

            string region = connections.GetSection("AwsCredentials").GetValue<string>("Region")!;
            var _region = Amazon.RegionEndpoint.GetBySystemName(region);

            _storageClass = connections.GetSection("AwsCredentials").GetValue<string>("StorageClass")!;

            _awsClient = new AmazonS3Client(
                                            new BasicAWSCredentials(accessKeyId, secretAccessKey),
                                            new AmazonS3Config { RegionEndpoint = _region }
                                            );
        }

        public async Task<IEnumerable<string>> ListAllBuckets()
        {
            var buckets = await _awsClient.ListBucketsAsync();
            return buckets.Buckets.Select(x => x.BucketName);
        }

        public async Task UploadFileAsync(IFormFile file, string fileName, string bucketName)
        {
            using MemoryStream inMemoryFile = new();

            file.CopyTo(inMemoryFile);

            TransferUtility fileTransferUtility = new(_awsClient);
            
            await fileTransferUtility.UploadAsync( 
                                        new TransferUtilityUploadRequest
                                        {
                                            InputStream = inMemoryFile,
                                            Key = fileName,
                                            BucketName = bucketName,
                                            ContentType = file.ContentType,
                                            StorageClass = new S3StorageClass(_storageClass)
                                        });
        }

        public async Task<IEnumerable<string>> ListAllFilesFromBucket(string bucketName)
        {
            var files = await _awsClient.ListObjectsAsync(bucketName);

            if (files == null)
                return new List<string> { "Empty" };

            return files.S3Objects.Select(x=>x.Key);
        }

        public async Task DownloadFile(string fileName, string bucketName)
        {
            TransferUtility transferUtility = new(_awsClient);

            await transferUtility.DownloadAsync(new TransferUtilityDownloadRequest
            {
                BucketName = bucketName,
                Key = fileName
            });
        }
        
        public async Task DeleteFileFromBucket(string fileName, string bucketName)
        {   
            var obj = new DeleteObjectRequest{ BucketName = bucketName, Key=fileName };
            await _awsClient.DeleteObjectAsync(obj);
        }
    }
}
