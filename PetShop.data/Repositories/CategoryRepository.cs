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
            throw new NotImplementedException();
        }

        public Category Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Category Get(int? id)
        {
            throw new NotImplementedException();
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
