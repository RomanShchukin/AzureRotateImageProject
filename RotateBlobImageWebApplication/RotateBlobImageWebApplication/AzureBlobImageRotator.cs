using Azure.Storage.Blobs;
using ImageReader;

namespace RotateBlobImageWebApplication
{
    public class AzureBlobImageRotator
    {

        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobImageRotator(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<string> RotateImage(string blobImagePath)
        {
            var storage = new AzureBlobStorage(_blobServiceClient);

            var blob = await storage.ReadBlob(blobImagePath);
            byte[] data = Convert.FromBase64String(blob.Content.ToString());
            var rotatedBlobData = ImageRotator.ImageRotator.Rotate(data, blob.Details.ContentType);

            return await storage.WriteBlob(blob.Details.ContentType, BinaryData.FromString(Convert.ToBase64String(rotatedBlobData)));
        }
    }
}
