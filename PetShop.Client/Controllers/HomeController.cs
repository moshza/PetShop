using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Client.Models;
using PetShop.data.Repositories;
using System.Diagnostics;

namespace PetShop.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnimalRepository _animalRepository;
        

        public HomeController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
          
        }

        public IActionResult Index()
        {
            var animals = _animalRepository.PopularAnimals(4);
            return View(animals);
        }
       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}