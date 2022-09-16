using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZayShop.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class CategoryController : Controller
    {
        private readonly ICategoryManager _categoryManager;
        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public IActionResult Index()
        {
            var categories = _categoryManager.GetAll();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category, IFormFile NewPhoto)
        {

            if(NewPhoto != null)
            {
                var categorySelected = _categoryManager.GetByName(category.Name);
                if (categorySelected == null)
                {


                    string myPhoto = Guid.NewGuid().ToString() + Path.GetExtension(NewPhoto.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/Img", myPhoto);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        NewPhoto.CopyTo(stream);
                    };

                    category.PhotoURL = "admin/Img/" + myPhoto;
                    _categoryManager.Add(category);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Name", "Eyni adli category ola bilmez");
                    return View(category);
                }
            }
            else
            {
                ViewBag.PhotoError = "Sekil bos ola bilmez";
                return View();
            }
           




        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _categoryManager.Get(id);
            if (category == null) return RedirectToAction("Index");
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category, IFormFile NewPhoto, string? oldPhoto, int Id)
        {
            var categoryName = _categoryManager.GetByName(category.Name);
            var categoryPhoto = _categoryManager.Get(Id);
            if (categoryName != null && category.Id != categoryName.Id)
            {
                ModelState.AddModelError("Name", "Eyni adli category ola bilmez");
                ViewBag.Photo = categoryPhoto.PhotoURL;
                ViewBag.oldPhoto = oldPhoto;
                return View(category);
            }
            else
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
                    category.PhotoURL = "admin/Img/" + myPhoto;
                }
                else category.PhotoURL = oldPhoto;

                _categoryManager.Update(category);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _categoryManager.Get(id);
            if (category == null) return NotFound();
            return View();
        }


        [HttpPost]
        public IActionResult Delete(Category category)
        {
            try
            {
                _categoryManager.Delete(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
