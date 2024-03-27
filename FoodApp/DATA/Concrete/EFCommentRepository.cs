using FoodApp.DATA.Abstract;
using FoodApp.DATA.Concrete.EfCore;
using FoodApp.Entity;

namespace FoodApp.DATA.Concrete
{
    public class EFCommentRepository : ICommentRepository
    {
        public readonly FoodContext _context;

        public EFCommentRepository(FoodContext context){
            _context = context;
        }

        public IQueryable<Comment> Comments => _context.Comments;

        public void CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }

}