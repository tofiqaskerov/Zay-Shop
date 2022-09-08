using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace ZayShop.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class BannerController : Controller
    {
        private readonly IBannerManager _bannerManager;

        public BannerController(IBannerManager bannerManager)
        {
            _bannerManager = bannerManager;
        }

        public IActionResult Index()
        {
            var banner = _bannerManager.GetAll();
            return View(banner);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var bannerCount = _bannerManager.GetBannerCount();
            if (bannerCount > 2) return RedirectToAction("Index");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Banner banner, IFormFile NewPhoto)
        {
            if (NewPhoto != null)
            {
                if (banner.Title != null)
                {
                    var fileExtation = Path.GetExtension(NewPhoto.FileName);
                    if (fileExtation != ".jpg")
                    {
                        ViewBag.PhotoError = "Yalniz jpg formati qebul olunur";
                        return View();
                    }
                    string myPhoto = Guid.NewGuid().ToString() + Path.GetExtension(NewPhoto.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/Img", myPhoto);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        NewPhoto.CopyTo(stream);
                    };
                    banner.PhotoURL = "admin/Img/" + myPhoto;
                    _bannerManager.Add(banner);
                    
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.EmptyTitle = "Basligi Doldurun";
                    return View();
                }

            }
            else
            {
                ViewBag.EmptyPhoto = "Sekil elave edin";
                return View();
            }
           
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {   
            var banner = _bannerManager.Get(id);
            if(banner == null) return RedirectToAction("Index");
            return View(banner);
        }
        [HttpPost]
        public IActionResult Edit(Banner banner, IFormFile NewPhoto, string? oldPhoto)
        {
            if (NewPhoto != null)
            {
                var fileExtation = Path.GetExtension(NewPhoto.FileName);

                string myPhoto = Guid.NewGuid().ToString() + Path.GetExtension(NewPhoto.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/Img", myPhoto);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    NewPhoto.CopyTo(stream);
                };
                banner.PhotoURL = "admin/Img/" + myPhoto;
            }
            else banner.PhotoURL = oldPhoto;

            _bannerManager.Update(banner);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var banner = _bannerManager.Get(id);
            if (banner == null) return RedirectToAction("Index");
            return View();

        }
        [HttpPost]
        public IActionResult Delete(Banner banner)
        {
            try
            {
                _bannerManager.Delete(banner);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
