using FoodApp.Entity;

namespace FoodApp.DATA.Abstract
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments {get;}

        void CreateComment(Comment comment);
    }
}