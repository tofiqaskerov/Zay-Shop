using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntitieFramework;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBannerManager, BannerManager>();
builder.Services.AddScoped<IBannerDal, BannerDal>();
builder.Services.AddScoped<ICategoryManager, CategoryManager>();
builder.Services.AddScoped<ICategoryDal, CategoryDal>();


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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();

