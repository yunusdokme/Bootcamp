using FoodApp.Entity;

namespace FoodApp.DATA.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> Users{get;}

        void CreatUser(User user);
    }
}