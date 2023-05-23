using PetShop.data.Context;
using PetShop.data.Models;

namespace PetShop.data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly PetShopDbContext _context;
        public CategoryRepository(PetShopDbContext context)
        {
            _context=context;
        }
        public Category Add(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public Category Delete(int id)
        {
            var category = _context.Categories.Single(c => c.CategoryId == id);
            _context.Remove(category);
            _context.SaveChanges();
            return category;
        }

        public Category Get(int? id)
        {
            return _context.Categories.SingleOrDefault(c => c.CategoryId == id)!;
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories;
        }

        public Category Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
