using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using X.Application.Core.Storage;

namespace X.Infrastructure.Storage
{
    internal class FileStorageService : IFileStorageService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FileStorageService> _logger;

        public FileStorageService(IConfiguration configuration, ILogger<FileStorageService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            var storageProvider = _configuration["StorageProvider"];
            switch (storageProvider.ToLower())
            {
                case "azure":
                    return await UploadToAzureBlobStorageAsync(fileStream, fileName, contentType);
                case "google":
                    return await UploadToGoogleCloudStorageAsync(fileStream, fileName, contentType);
                case "aws":
                    return await UploadToAwsS3Async(fileStream, fileName, contentType);
                default:
                    throw new ArgumentException("Unsupported storage provider");
            }
        }

        public async Task<Stream> DownloadFileAsync(string fileName)
        {
            var storageProvider = _configuration["StorageProvider"];
            switch (storageProvider.ToLower())
            {
                case "azure":
                    return await DownloadFromAzureBlobStorageAsync(fileName);
                case "google":
                    return await DownloadFromGoogleCloudStorageAsync(fileName);
                case "aws":
                    return await DownloadFromAwsS3Async(fileName);
                default:
                    throw new ArgumentException("Unsupported storage provider");
            }
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var storageProvider = _configuration["StorageProvider"];
            switch (storageProvider.ToLower())
            {
                case "azure":
                    await DeleteFromAzureBlobStorageAsync(fileName);
                    break;
                case "google":
                    await DeleteFromGoogleCloudStorageAsync(fileName);
                    break;
                case "aws":
                    await DeleteFromAwsS3Async(fileName);
                    break;
                default:
                    throw new ArgumentException("Unsupported storage provider");
            }
        }

        private async Task<string> UploadToAzureBlobStorageAsync(Stream fileStream, string fileName, string contentType)
        {
            var connectionString = _configuration["AzureBlobStorage:ConnectionString"];
            var containerName = _configuration["AzureBlobStorage:ContainerName"];
            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });
            return blobClient.Uri.ToString();
        }

        private async Task<Stream> DownloadFromAzureBlobStorageAsync(string fileName)
        {
            var connectionString = _configuration["AzureBlobStorage:ConnectionString"];
            var containerName = _configuration["AzureBlobStorage:ContainerName"];
            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            var downloadInfo = await blobClient.DownloadAsync();
            return downloadInfo.Value.Content;
        }

        private async Task DeleteFromAzureBlobStorageAsync(string fileName)
        {
            var connectionString = _configuration["AzureBlobStorage:ConnectionString"];
            var containerName = _configuration["AzureBlobStorage:ContainerName"];
            var blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            await blobClient.DeleteAsync();
        }

        private async Task<string> UploadToGoogleCloudStorageAsync(Stream fileStream, string fileName, string contentType)
        {
            var projectId = _configuration["GoogleCloudStorage:ProjectId"];
            var bucketName = _configuration["GoogleCloudStorage:BucketName"];
            var storageClient = await StorageClient.CreateAsync();
            var objectName = fileName;

            await storageClient.UploadObjectAsync(bucketName, objectName, contentType, fileStream);
            return $"https://storage.googleapis.com/{bucketName}/{objectName}";
        }

        private async Task<Stream> DownloadFromGoogleCloudStorageAsync(string fileName)
        {
            var projectId = _configuration["GoogleCloudStorage:ProjectId"];
            var bucketName = _configuration["GoogleCloudStorage:BucketName"];
            var storageClient = await StorageClient.CreateAsync();
            var memoryStream = new MemoryStream();

            await storageClient.DownloadObjectAsync(bucketName, fileName, memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }

        private async Task DeleteFromGoogleCloudStorageAsync(string fileName)
        {
            var projectId = _configuration["GoogleCloudStorage:ProjectId"];
            var bucketName = _configuration["GoogleCloudStorage:BucketName"];
            var storageClient = await StorageClient.CreateAsync();

            await storageClient.DeleteObjectAsync(bucketName, fileName);
        }

        private async Task<string> UploadToAwsS3Async(Stream fileStream, string fileName, string contentType)
        {
            var accessKey = _configuration["AWS:AccessKey"];
            var secretKey = _configuration["AWS:SecretKey"];
            var region = _configuration["AWS:Region"];
            var bucketName = _configuration["AWS:BucketName"];

            var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fileStream,
                Key = fileName,
                BucketName = bucketName,
                ContentType = contentType
            };

            var fileTransferUtility = new TransferUtility(s3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);
            return $"https://{bucketName}.s3.{region}.amazonaws.com/{fileName}";
        }

        private async Task<Stream> DownloadFromAwsS3Async(string fileName)
        {
            var accessKey = _configuration["AWS:AccessKey"];
            var secretKey = _configuration["AWS:SecretKey"];
            var region = _configuration["AWS:Region"];
            var bucketName = _configuration["AWS:BucketName"];

            var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));
            var downloadRequest = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = fileName
            };

            var response = await s3Client.GetObjectAsync(downloadRequest);
            var memoryStream = new MemoryStream();
            await response.ResponseStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }

        private async Task DeleteFromAwsS3Async(string fileName)
        {
            var accessKey = _configuration["AWS:AccessKey"];
            var secretKey = _configuration["AWS:SecretKey"];
            var region = _configuration["AWS:Region"];
            var bucketName = _configuration["AWS:BucketName"];

            var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = fileName
            };

            await s3Client.DeleteObjectAsync(deleteRequest);
        }
    }
}