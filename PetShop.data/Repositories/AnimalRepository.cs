using Microsoft.EntityFrameworkCore;
using PetShop.data.Context;
using PetShop.data.Models;

namespace PetShop.data.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        readonly PetShopDbContext _context;
        public AnimalRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public Animal Add(Animal entity)
        {
           _context.Animals.Add(entity);
           _context.SaveChanges();

            return entity;
        }

        public void AddComment(string comment)
        {
            
        }

        public Animal Delete(int id)
        {
            var animal=_context.Animals.Single(a => a.AnimalId == id);
            _context.Animals.Remove(animal);
            _context.SaveChanges();
            return animal;
        }

        public Animal Get(int? id)
        {
            return _context.Animals
                           .Include(a => a.Category)
                           .Include(a => a.Comments)
                           .FirstOrDefault(b => b.AnimalId == id)!;
        }

        public IQueryable<Animal> GetAll()
        {
           return _context.Animals
                          .Include(a => a.Category)
                          .Include(a => a.Comments);
        }
        public IQueryable<Animal> PopularAnimals(int count)
        {
            return _context.Animals
                           .Include(a => a.Category)
                           .Include(a => a.Comments)
                           .OrderByDescending(a => a.Comments.Count).Take(count);
        }

        public Animal Update(Animal entity)
        {
            try
            {
                _context.Animals.Update(entity);
                _context.SaveChanges();

                return entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsAnimalExist(entity.AnimalId))
                {
                    return null!;
                }
                else
                {
                    throw;
                }
            }
        }

        public bool IsAnimalExist(int id)
        {
            return _context.Animals.Any(a => a.AnimalId == id);
        }

        public IQueryable<Animal> GetAnimalByCategory(int categoryId)
        {
           return _context.Animals.Where(a=> a.CategoryId == categoryId);
        }
    }
}

