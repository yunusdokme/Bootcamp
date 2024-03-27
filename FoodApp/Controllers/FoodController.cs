
using System.Security.Claims;
using FoodApp.DATA.Abstract;
using FoodApp.Entity;
using FoodApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.Controllers
{
    public class FoodController : Controller
    {
        private IFoodRepository _foodRepository;
        private ICommentRepository _commentRepository;
        private  ICategoryRepository _categoriesRepository;

        public FoodController(IFoodRepository foodRepository, ICommentRepository commentRepository,ICategoryRepository categoryRepository){
            _foodRepository=foodRepository;
            _commentRepository=commentRepository;
            _categoriesRepository=categoryRepository;
        }

        public async Task<IActionResult> Index(string category)
        {
         
             var claims = User.Claims;
             var food = _foodRepository.Foods;
            
             if(!string.IsNullOrEmpty(category))
        {
                 food = food.Where(x=>x.Categories.Any(c=>c.Url == category));

             }
            return View(new FoodViewModel{Foods= await food.ToListAsync()});
            


        }

        public async Task<IActionResult>Details(string url){
            return View(await _foodRepository
                              .Foods
                              .Include(x=>x.Categories)
                              .Include(x=>x.Comments)
                              .FirstOrDefaultAsync(p=>p.Url == url)     
                              );
        }

        [HttpPost]
        public IActionResult AddComment( int FoodId, string Text){
            var userId= User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var entity = new Comment{
                FoodId=FoodId,
                Text= Text,
                PublishedOn= DateTime.Now,
                UserId = int.Parse(userId ?? "")
            };
            _commentRepository.CreateComment(entity);
            return RedirectToAction("Index", "Home");


        }
        [Authorize]
        public IActionResult Create(){
            return View();
        }

        [HttpPost]
[Authorize]
public IActionResult Create(FoodCreateViewModel model)
{
    if(ModelState.IsValid)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        _foodRepository.CreateFood(
            new Food{
                Title=model.Title,
                Content = model.Content ,
                UserId = int.Parse(userId ?? ""),
                Price = model.Price

            }
        );
        return RedirectToAction("Index","Food");
    }
    return View(model);
}


        [Authorize]
        public async Task<IActionResult> List()

        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var role = User.FindFirstValue(ClaimTypes.Role);

            var food= _foodRepository.Foods;

            if(string.IsNullOrEmpty(role)){
                food =food.Where(i=>i.UserId==userId);
            }

            return View(await food.ToListAsync());

        }

        [Authorize]
        public IActionResult Edit(int? id){
            if(id == null)
            {
                return NotFound();
            }
            var food = _foodRepository.Foods.Include(i=>i.Categories).FirstOrDefault(i=>i.FoodId == id);
            if(food == null){
                return NotFound();
            }
            ViewBag.Categories= _categoriesRepository.Categories.ToList();
              
              return View( new FoodCreateViewModel{
                FoodId=food.FoodId,
                Title=food.Title,
                Content =food.Content,
                Categories = food.Categories
              });
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(FoodCreateViewModel model, int[] tagId){
            if(ModelState.IsValid){
                var entityUpdate = new Food{
                    FoodId = model.FoodId,
                    Title = model.Title,
                    Content = model.Content ,
                    Url = model.Url
                };

               

                _foodRepository.EditFood(entityUpdate,tagId);
                return RedirectToAction("list");
            }

            ViewBag.Categories = _categoriesRepository.Categories.ToList();
            return View(model);
        }
        

    }
}