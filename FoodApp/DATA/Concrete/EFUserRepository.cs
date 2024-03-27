using FoodApp.DATA.Abstract;
using FoodApp.DATA.Concrete.EfCore;
using FoodApp.Entity;

namespace FoodApp.DATA.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        private readonly FoodContext _contrxt;

        public EFUserRepository(FoodContext context){
            _contrxt=context;
        }

        public IQueryable<User> Users => _contrxt.Users;

        public void CreatUser(User user)
        {
            _contrxt.Users.Add(user);
            _contrxt.SaveChanges();
        }
    }
}