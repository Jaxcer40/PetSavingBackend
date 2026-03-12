using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSavingBackend.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace PetSavingBackend.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _env;
        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public bool DeleteImage(Guid id)
        {
            string sourceFolder = Path.Combine(_env.WebRootPath, "Images/source");

            string smFolder = Path.Combine(_env.WebRootPath, "Images/sm");

            string? contentType = GetImageExtension(id);

            if (contentType == null) { contentType = ".png"; }

            string sourceFilePath = Path.Combine(sourceFolder, id.ToString() + contentType);

            string smFilePath = Path.Combine(smFolder, id.ToString() + ".jpg");

            if (File.Exists(sourceFilePath) && File.Exists(smFilePath))
            {
                File.Delete(sourceFilePath);

                File.Delete(smFilePath);

                return true;
            }
            else
            {
                return false;
            }
        }

        public string? GetImageExtension(Guid id)
        {
            string directoryPath = Path.Combine(_env.WebRootPath, "Images/source/");

            string searchPattern = id.ToString() + ".*";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string[] filePath = Directory.GetFiles(directoryPath, searchPattern);

            if (filePath.Length == 0)
            {
                return null;
            }

            if (!File.Exists(filePath[0]))
            {
                return null;
            }

            return Path.GetExtension(filePath[0].ToLowerInvariant());
        }

        public bool IsImageBySignature(Stream stream)
        {
            // Ensure the stream is at the beginning
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            byte[] headerBytes = new byte[8];
            stream.Read(headerBytes, 0, headerBytes.Length);

            // PNG signature
            byte[] png = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
            if (headerBytes.SequenceEqual(png))
            {
                return true;
            }

            // JPEG signature (first three bytes)
            byte[] jpeg = { 0xFF, 0xD8, 0xFF };
            if (headerBytes[0] == jpeg[0] && headerBytes[1] == jpeg[1] && headerBytes[2] == jpeg[2])
            {
                return true;
            }

            return false;
        }

        public Image ToSmallFromSourceAsync(IFormFile imageFile)
        {
            int targetWidth = 128;
            int targetHeight = 128;

            var imageStream = imageFile.OpenReadStream();

            Image image = Image.Load(imageStream);

            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(targetWidth, targetHeight),
                Mode = ResizeMode.Crop,      // Fill the square and crop excess
                Position = AnchorPositionMode.Center // Crop from center
            }));

            return image;
        }

        public async Task<bool> WriteIFormFileAsync(IFormFile sourceFile, Guid id)
        {
            string uploadsFolder = Path.Combine(_env.WebRootPath, "Images/source");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileExtension = Path.GetExtension(sourceFile.FileName);

            var sourceFilePath = Path.Combine(uploadsFolder, id.ToString() + fileExtension);

            using (var stream = new FileStream(sourceFilePath, FileMode.Create))
            {
                await sourceFile.CopyToAsync(stream);
            }

            return true;
        }

        public async Task<bool> WriteImageAsync(Image imageFileToWrite, Guid id)
        {
            int jpegQuality = 100; // 0 (lowest quality/size) to 100 (highest quality/size)

            var outputStream = new MemoryStream();

            // Save the image as JPEG with specified quality
            var jpegEncoder = new JpegEncoder
            {
                Quality = jpegQuality
            };

            await imageFileToWrite.SaveAsync(outputStream, jpegEncoder);

            // Convert the processed image to a byte array (optional: for storage)
            var imageBytes = outputStream.ToArray();

            // You can store imageBytes somewhere (e.g., Azure Blob Storage, S3, or disk)
            // Example of saving to disk:
            string uploadsFolder = Path.Combine(_env.WebRootPath, "Images/sm");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var smFilePath = Path.Combine(uploadsFolder, id.ToString() + ".jpg");

            await File.WriteAllBytesAsync(smFilePath, imageBytes);

            return true;
        }
    }
}