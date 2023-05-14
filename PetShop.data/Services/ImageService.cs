using Microsoft.AspNetCore.Http;
using PetShop.data.Models;

namespace PetShop.data.Services
{
    public class ImageService : IImageService
    {
        readonly IStorageService _storageService;
        public ImageService(IStorageService storageService)
        {
            _storageService = storageService;
        }
        //puts photos folder on wwwroot
        string ImageDir => Path.Combine(_storageService.AbsolutePath, "Photos");

        //puts  category name under Image folder
        string CategoryDir(string name) => Path.Combine(ImageDir, name);

        
        public string GetFullImageUrl(Animal animal) => Path.Combine(CategoryDir(animal.Category.Name), animal.PhotoUrl ?? "");


        public Task EnsureDirCreated(Category category)
        {
            Directory.CreateDirectory(CategoryDir(category.Name));
            return Task.CompletedTask;
        }
        public void DeleteImage(Animal animal)
        {
            string fullPath = GetFullImageUrl(animal);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
        public async Task<(bool, string)> UploadImage(IFormFile imageFile, Animal animal)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = $"{imageFile.FileName}";

                //create file path
                var categoryPath = CategoryDir(animal.Category.Name);
                await EnsureDirCreated(animal.Category);
                string fullPath = Path.Combine(categoryPath, fileName);

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                string imagePath = fullPath.ToString();
                string result = Path.GetFileName(imagePath);
                return (true, result);
            }
            return (false, String.Empty);
        }
    }
}
