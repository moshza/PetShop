#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Client.Models;
using PetShop.data.Models;
using PetShop.data.Repositories;

namespace PetShop.Client.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IAnimalRepository _context;
        private readonly ICategoryRepository _contextCategory;
        private readonly ICommentRepository _contextComment;

        public AnimalsController(IAnimalRepository context, ICategoryRepository contextCategory, ICommentRepository contextComment)
        {
            _context = context;
            _contextCategory = contextCategory;
            _contextComment=contextComment;
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


        // GET: Animals/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_contextCategory.GetAll(), "CategoryId", "Name");
            return View();
        }

        // POST: Animals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("AnimalId,Name,BirthDate,Description,CategoryId,PhotoUrl")] Animal animal)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(animal);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_contextCategory.GetAll(), "CategoryId", "Name", animal.CategoryId);
            return View(animal);
        }

        // GET: Animals/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = _context.Get(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_contextCategory.GetAll(), "CategoryId", "Name", animal.CategoryId);
            return View(animal);
        }

        // POST: Animals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("AnimalId,Name,BirthDate,Description,CategoryId,PhotoUrl")] Animal animal)
        {
            if (id != animal.AnimalId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.AnimalId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_contextCategory.GetAll(), "CategoryId", "Name", animal.CategoryId);
            return View(animal);
        }

        // GET: Animals/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = _context.Get(id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var animal = _context.Get(id);
            _context.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.GetAll().Any(e => e.AnimalId == id);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var commentModel = new AddCommentModel();
            var animal =_context.Get(id);
            commentModel.Animal = animal;
            if (animal == null)
                return NotFound();

            return View(commentModel);

        }
        [HttpPost]
        public IActionResult Details(int id, [FromForm] AddCommentModel newComment)
        {
            if (!ModelState.IsValid)
            {
                var Comment = _contextComment.Create(newComment.Content, id);
                if (Comment.AnimalId == 0)
                    return NoContent();
                return RedirectToAction(nameof(Details), new { id = Comment.AnimalId });
            }
            else
            {
                return NoContent();
            }

        }

    }
}
