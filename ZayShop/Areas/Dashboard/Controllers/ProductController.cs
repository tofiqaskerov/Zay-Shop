﻿using Microsoft.AspNetCore.Mvc;

namespace ZayShop.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
