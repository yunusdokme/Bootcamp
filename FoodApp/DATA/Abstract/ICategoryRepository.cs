

using FoodApp.Entity;

namespace FoodApp.DATA.Abstract
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories {get;}

        void CreateFood(Category category);
    }
}