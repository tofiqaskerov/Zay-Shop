using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZayShop.Models;
using ZayShop.ViewModels;

namespace ZayShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBannerManager _bannerManager;
        private readonly ICategoryManager _categoryManager;


        public HomeController(ILogger<HomeController> logger, IBannerManager bannerManager, ICategoryManager categoryManager)
        {
            _logger = logger;
            _bannerManager = bannerManager;
            _categoryManager = categoryManager;
        }

        public IActionResult Index()
        {
            var banners = _bannerManager.GetAll();
            var categories = _categoryManager.GetAll();
            HomeVM homeVM = new()
            {
                Banners = banners,
                Categories = categories
            };

            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}