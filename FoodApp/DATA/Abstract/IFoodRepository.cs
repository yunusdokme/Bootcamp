using FoodApp.Entity;

namespace FoodApp.DATA.Abstract
{
    public interface IFoodRepository
    {
        IQueryable<Food> Foods {get;}

        void CreateFood(Food food);
        void EditFood(Food food, int[] categoryId);

    }

}