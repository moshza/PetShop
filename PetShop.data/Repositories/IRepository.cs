using PetShop.data.Models;

namespace PetShop.data.Repositories
{
    public interface IRepository<T>where T : class
    {
        T Get(int? id);
        T Update(T entity);
        T Add(T entity);
        T Delete(int id);  
        IQueryable<T> GetAll();
    }
    public interface IAnimalRepository : IRepository<Animal>
    {
        void AddComment(string comment);
        IQueryable<Animal> PopularAnimals(int count);
        IQueryable<Animal> GetAnimalByCategory(int categoryId);
    }
    public interface ICategoryRepository : IRepository<Category>
    {

    }
    public interface ICommentRepository : IRepository<Comment>
    {
        Comment Create(string content, int Id);
    }
}
