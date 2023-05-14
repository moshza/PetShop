using Microsoft.AspNetCore.Http;
using PetShop.data.Models;

namespace PetShop.data.Services
{
    public interface IImageService
    {
        Task EnsureDirCreated(Category category);
        Task<(bool, string)> UploadImage(IFormFile imageFile, Animal animal);
        void DeleteImage(/*IFormFile imageFile,*/ Animal animal);
        /*Task<string> NewFileLocation(string oldPath, string newPath);*/
        string GetFullImageUrl(Animal animal);
    }
}
