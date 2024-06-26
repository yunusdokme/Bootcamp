using FoodApp.DATA.Abstract;
using FoodApp.Models;
using FoodApp.Entity;
using FoodApp.DATA.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace FoodApp.Controllers
{
    public class UserController:Controller{

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository){
            _userRepository= userRepository;
        }
        
        public IActionResult Login()
        {
            if(User.Identity!.IsAuthenticated){
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid){
                var user = await _userRepository.Users.FirstOrDefaultAsync(x=>x.UserName == model.UserName || x.Email == model.Email);
                if(user == null)
                {
                    _userRepository.CreatUser(new User{
                        UserName = model.UserName,
                        Name = model.Name,
                        Email = model.Email,
                        Password = model.Password
                    });
                    return RedirectToAction("Login");
                }
                else{
                    ModelState.AddModelError("","Username veya Email kullanımda");
                }
                
            }
            return View(model);
        }

        public async Task<IActionResult>Loguot()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult>Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var isUser = await _userRepository.Users.FirstOrDefaultAsync(x=>x.Email == model.Email && x.Password == model.Password);

                if(isUser !=null)
                {
                    var userClaims = new List<Claim>();
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier,isUser.UserId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));
                    if(isUser.Email == "info@admin.com"){
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }

                    var claimsIdentity = new ClaimsIdentity(userClaims,CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties{
                        IsPersistent=true
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                       new ClaimsPrincipal(claimsIdentity),authProperties);

                    return RedirectToAction("Index","Food");


                }
                else{
                    ModelState.AddModelError("","Kullanıcı adı veya şifre hatalı");
                }
                
            }
            return View(model);
        }

        


    }
}