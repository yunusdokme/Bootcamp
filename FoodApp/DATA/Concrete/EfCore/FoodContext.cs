using FoodApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.DATA.Concrete.EfCore
{
    public class FoodContext:DbContext
    {
        public FoodContext(DbContextOptions<FoodContext> options):base(options){

        }

        public DbSet<Food> Foods => Set<Food>();
        public DbSet<User> Users=> Set<User>();
        public DbSet<Comment> Comments => Set<Comment>();
         public DbSet<Category> Categories => Set<Category>();
    }

}