
using FoodApp.DATA.Abstract;
using FoodApp.DATA.Concrete.EfCore;
using FoodApp.Entity;

namespace FoodApp.DATA.Concrete
{
    public class EFCategoryRepository :ICategoryRepository
    {
        private readonly FoodContext _context;

        public EFCategoryRepository(FoodContext context)
        {
            _context=context;
        }
        public IQueryable<Category> Categories => _context.Categories;

        public void CreateFood(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

        }
    }
}