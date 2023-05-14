using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Client.Models;
using PetShop.data.Repositories;

namespace PetShop.Client.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAnimalRepository _context;
        private readonly ICategoryRepository _contextCategory;
        public AdminController(IAnimalRepository context, ICategoryRepository contextCategory)
        {
            _context = context; 
            _contextCategory=contextCategory;
        }
        public IActionResult Index()
        {
            CategoriesViewModel vm = new CategoriesViewModel();
            ViewBag.CategoryId = new SelectList(_contextCategory.GetAll(), "CategoryId", "Name");
            vm.Animals = _context.GetAll();
            vm.Category = 0;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Index([FromForm] CategoriesViewModel vm)
        {
            ViewBag.CategoryId = new SelectList(_contextCategory.GetAll(), "CategoryId", "Name");
            if (vm.Category > 0)
            {
                vm.Animals = _context.GetAnimalByCategory(vm.Category);
                return View(vm);
            }
            vm.Animals = _context.GetAll();
            return View(vm);
        }
    }
}
