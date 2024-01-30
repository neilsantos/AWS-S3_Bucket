using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using S3ManagementAPI.Domain;

namespace BucketMgmt_AWS_S3.Infra
{
    public class AwsClient : IAwsClient
    {
        private readonly IAmazonS3 _awsClient;
        private readonly string _storageClass;

        public AwsClient(IConfiguration connections)
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

        #region Buckets
        public async Task<IEnumerable<string>> ListAllBuckets()
        {
            var buckets = await _awsClient.ListBucketsAsync();
            return buckets.Buckets.Select(x => x.BucketName);
        }
        public async Task CreateBucket(string bucketName)
        {
            await _awsClient.PutBucketAsync(bucketName);
        }

        public async Task DeleteBucket(string bucketName)
        {
            await _awsClient.DeleteBucketAsync(bucketName);
        }


        #endregion


        #region Files
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

            return files.S3Objects.Select(x => x.Key);
        }

        public async Task<IEnumerable<string>> GetFilesUrl(string bucketName,string fileName="")
        {
            var request = new ListObjectsV2Request(){
                                                        BucketName = bucketName,
                                                    };

            var result = await _awsClient.ListObjectsV2Async(request);
            var files = result.S3Objects.Select(s=>s.Key);
           
            if (!string.IsNullOrEmpty(fileName))
                files = files.Where(x => x == fileName);

            var urls = files.Select(s =>
            {
                var urlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketName,
                    Key = s,
                    Expires = DateTime.UtcNow.AddMinutes(1)
                };
                return _awsClient.GetPreSignedURL(urlRequest);
            });
            return urls;
        }

        public async Task DeleteFileFromBucket(string fileName, string bucketName)
        {
            var obj = new DeleteObjectRequest { BucketName = bucketName, Key = fileName };
            await _awsClient.DeleteObjectAsync(obj);
        }

        #endregion

        public async Task<bool> DoesThisBucketExist(string bucketName)
        {
            return await AmazonS3Util.DoesS3BucketExistV2Async(_awsClient, bucketName);
        }

    }
}
