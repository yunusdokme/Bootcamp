

using FoodApp.DATA.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FoodApp.ViewComponents
{
    public class CategoryMenu : ViewComponent
    {
        private ICategoryRepository _categoryRepositor;

        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepositor = categoryRepository;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _categoryRepositor.Categories.ToListAsync());
        }
    }
}