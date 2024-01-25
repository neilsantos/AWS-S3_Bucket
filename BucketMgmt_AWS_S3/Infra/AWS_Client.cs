using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace BucketMgmt_AWS_S3.Infra
{
    public class AWS_Client : IAWS_Client
    {

        private readonly IAmazonS3 _awsClient;

        public AWS_Client(IConfiguration connections)
        {
            string Uid = connections.GetSection("AwsCredentials").GetValue<string>("Uid")!;
            string key = connections.GetSection("AwsCredentials").GetValue<string>("Key")!;

            _awsClient = new AmazonS3Client(
                                            new BasicAWSCredentials(Uid, key),
                                            new AmazonS3Config { RegionEndpoint = Amazon.RegionEndpoint.USEast2 }
                                            );
        }

        public async Task<bool> UploadFileAsync(IFormFile file, string fileName, string bucketName)
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
                                            StorageClass = new S3StorageClass("INTELLIGENT_TIERING")
                                        });
            return true;
        }
    }
}
