using ImageReader;
using System.Drawing.Imaging;
using System.Text;
using ImageRotator;

namespace RotatBlobImageTestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestReabBlob()
        {
            var reader = new AzureBlobStorage();

            var blob = await reader.ReadBlob("2c00fda4-92c6-4b4d-9f0d-8a7598c7fb90");

            byte[] data = Convert.FromBase64String(blob.Content.ToString());
            string decodedString = Encoding.UTF8.GetString(data);

            Assert.Pass();
        }

        [Test]
        public async Task TestReabBlobAndRotate()
        {
            var reader = new AzureBlobStorage();

            var blob = await reader.ReadBlob("2c00fda4-92c6-4b4d-9f0d-8a7598c7fb90");

            byte[] data = Convert.FromBase64String(blob.Content.ToString());

            var result = ImageRotator.ImageRotator.Rotate(data, blob.Details.ContentType);

            File.WriteAllBytes("C:\\Users\\Roman\\Documents\\test2.png", result);

            Assert.Pass();
        }

        [Test]
        public async Task TestReabBlobAndWriteRotate()
        {
            var storage = new AzureBlobStorage();

            var blob = await storage.ReadBlob("2c00fda4-92c6-4b4d-9f0d-8a7598c7fb90");
            byte[] data = Convert.FromBase64String(blob.Content.ToString());
            var rotatedBlobData = ImageRotator.ImageRotator.Rotate(data, blob.Details.ContentType);

            await storage.WriteBlob(blob.Details.ContentType, BinaryData.FromString(Convert.ToBase64String(rotatedBlobData)));
        }
    }
}