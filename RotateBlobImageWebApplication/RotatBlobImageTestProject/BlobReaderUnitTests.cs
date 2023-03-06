using ImageReader;
using System.Text;
using Azure.Storage.Blobs;
using Azure.Identity;

namespace RotatBlobImageTestProject
{
    public class Tests
    {
        private BlobServiceClient _blobServiceClient;
        [SetUp]
        public void Setup()
        {
            _blobServiceClient = new BlobServiceClient(
                new Uri("https://rshchukinstorageaccount.blob.core.windows.net"),
                new DefaultAzureCredential());
        }

        [Test]
        public async Task TestReabBlob()
        {

            var reader = new AzureBlobStorage(_blobServiceClient);

            var blob = await reader.ReadBlob("004a8791-78bc-4a64-801b-510b6603c88d");

            byte[] data = Convert.FromBase64String(blob.Content.ToString());
            string decodedString = Encoding.UTF8.GetString(data);

            Assert.Pass();
        }

        [Test]
        public async Task TestReabBlobAndRotate()
        {
            var reader = new AzureBlobStorage(_blobServiceClient);

            var blob = await reader.ReadBlob("004a8791-78bc-4a64-801b-510b6603c88d");

            byte[] data = Convert.FromBase64String(blob.Content.ToString());

            var result = ImageRotator.ImageRotator.Rotate(data, blob.Details.ContentType);

            File.WriteAllBytes("C:\\Users\\Roman\\Documents\\test2.png", result);

            Assert.Pass();
        }

        [Test]
        public async Task TestReabBlobAndWriteRotate()
        {
            var storage = new AzureBlobStorage(_blobServiceClient);

            var blob = await storage.ReadBlob("004a8791-78bc-4a64-801b-510b6603c88d");
            byte[] data = Convert.FromBase64String(blob.Content.ToString());
            var rotatedBlobData = ImageRotator.ImageRotator.Rotate(data, blob.Details.ContentType);

            await storage.WriteBlob(blob.Details.ContentType, BinaryData.FromString(Convert.ToBase64String(rotatedBlobData)));
        }
    }
}