using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;

namespace PetSavingBackend.Interfaces
{
    public interface IImageService
    {
        bool IsImageBySignature(Stream stream);
        string? GetImageExtension(Guid id);
        Image ToSmallFromSourceAsync(IFormFile imageFile);
        Task<bool> WriteImageAsync(Image imageFileToWrite, Guid id);
        Task<bool> WriteIFormFileAsync(IFormFile sourceFile, Guid id);
        bool DeleteImage(Guid id);
    }
}