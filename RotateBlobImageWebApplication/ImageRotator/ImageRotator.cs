using System.Drawing;
using System.Drawing.Imaging;

namespace ImageRotator
{
    public static class ImageRotator
    {
        public static byte[] Rotate(byte[] imageData, ImageFormat imageFormat)
        {
            using var inStream = new MemoryStream(imageData);

            var image = Image.FromStream(inStream);

            image.RotateFlip(RotateFlipType.Rotate180FlipX);

            using var outStream = new MemoryStream();
                
            image.Save(outStream, imageFormat);

            return outStream.ToArray();
        }
        public static byte[] Rotate(byte[] imageData, string mediaType)
        {
            return Rotate(imageData, ImageFormatFromImageMediaType(mediaType));
        }

        private static ImageFormat ImageFormatFromImageMediaType(string mediaType) =>
            mediaType switch
            {
                "image/bmp" => ImageFormat.Bmp,
                "image/jpeg" => ImageFormat.Jpeg,
                "image/png" => ImageFormat.Png,
                "image/tiff" => ImageFormat.Tiff,
                _ => throw new NotImplementedException()
            };
    }
}