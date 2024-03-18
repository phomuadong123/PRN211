﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreWebApp.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace StoreWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            var name = User.Claims.SingleOrDefault(c => c.Type.Equals(ClaimTypes.Name)).Value;
            if (name.Equals("Admin"))
            {
                return RedirectToAction("Index", "Member");
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
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
