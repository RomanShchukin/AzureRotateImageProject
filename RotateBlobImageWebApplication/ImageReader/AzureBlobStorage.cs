using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace ImageReader
{
    public class AzureBlobStorage
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobStorage(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<BlobDownloadResult> ReadBlob(string blobPath)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("images");

            BlobClient blobClient = containerClient.GetBlobClient(blobPath);

            var result = await blobClient.DownloadContentAsync();

            return result;
        }

        public async Task<string> WriteBlob(string contentType, BinaryData content)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("images");

            string blobPath = Guid.NewGuid().ToString();

            BlobClient blobClient = containerClient.GetBlobClient(blobPath);
            await blobClient.UploadAsync(content);
            blobClient.SetHttpHeaders(new BlobHttpHeaders { ContentType = contentType });

            return blobPath;
        }
    }
}