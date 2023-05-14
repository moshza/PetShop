using Microsoft.EntityFrameworkCore;
using PetShop.data.Context;
using PetShop.data.Models;

namespace PetShop.data.Repositories
{
    public class ComentRepository : ICommentRepository
    {
        readonly PetShopDbContext _context;
        public ComentRepository(PetShopDbContext context)
        {
            _context=context;
        }
        public Comment Add(Comment entity)
        {
            _context.Comments.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public Comment Create(string content, int id)
        {
            var newComment = new Comment
            {
                AnimalId = id,
                Content = content
            };
            _context.Comments.Add(newComment);
            _context.SaveChanges();
            return newComment;
        }
        public Comment Delete(int id)
        {
            var comment = _context.Comments.FirstOrDefault(a => a.CommentId == id);

            if (comment == null)
                return comment!;

            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return comment;
        }

        public Comment Get(int? id)
        {
            return _context.Comments.FirstOrDefault(c => c.CommentId == id)!;
               
        }

        public IQueryable<Comment> GetAll()
        {
            return _context.Comments.Include(c => c.Animal);
        }

        public Comment Update(Comment entity)
        {
           _context.Comments.Update(entity);
           _context.SaveChanges();

            return entity;
        }
    }
}
