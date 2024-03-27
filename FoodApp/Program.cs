using FoodApp.DATA.Abstract;
using FoodApp.DATA.Concrete;
using FoodApp.DATA.Concrete.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FoodContext>(options =>{options.UseSqlite(builder.Configuration["ConnectionStrings:Sql_connection"]);});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddScoped<IUserRepository,EFUserRepository>();
builder.Services.AddScoped<IFoodRepository, EFFoodRepository>();
builder.Services.AddScoped<ICommentRepository,EFCommentRepository>();
builder.Services.AddScoped<ICategoryRepository,EFCategoryRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Food}/{action=Index}/{id?}");

app.Run();
