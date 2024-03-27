using FoodApp.DATA.Abstract;
using FoodApp.DATA.Concrete.EfCore;
using FoodApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.DATA.Concrete
{
    public class EFFoodRepository : IFoodRepository
    {
        private readonly FoodContext _context;
        
        public EFFoodRepository(FoodContext context){
            _context =context;
        }

        public IQueryable<Food> Foods => _context.Foods;

        public void CreateFood(Food food)
        {
            _context.Foods.Add(food);
            _context.SaveChanges();
        }

        public void EditFood(Food food, int[] categoryId)
        {
            var entity = _context.Foods.Include(i=>i.Categories).FirstOrDefault(i=>i.FoodId == food.FoodId);

            if(entity !=null){
                entity.Title=food.Title;
                entity.Content=food.Content;
                entity.Price=food.Price;

                entity.Categories= _context.Categories.Where(category => categoryId.Contains(category.CategoryId)).ToList();

                _context.SaveChanges();
            }
        }

    }
}