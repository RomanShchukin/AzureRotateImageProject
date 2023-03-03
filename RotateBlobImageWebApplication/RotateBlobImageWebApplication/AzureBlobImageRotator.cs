using ImageReader;

namespace RotateBlobImageWebApplication
{
    public class AzureBlobImageRotator
    {
        public async Task<string> RotateImage(string blobImagePath)
        {
            var storage = new AzureBlobStorage();

            var blob = await storage.ReadBlob(blobImagePath);
            byte[] data = Convert.FromBase64String(blob.Content.ToString());
            var rotatedBlobData = ImageRotator.ImageRotator.Rotate(data, blob.Details.ContentType);

            return await storage.WriteBlob(blob.Details.ContentType, BinaryData.FromString(Convert.ToBase64String(rotatedBlobData)));
        }
    }
}
