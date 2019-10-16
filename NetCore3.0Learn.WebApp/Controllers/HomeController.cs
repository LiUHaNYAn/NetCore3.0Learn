using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCore3._0Learn.WebApp.Data;
using NetCore3._0Learn.WebApp.Data.Model;
using NetCore3._0Learn.WebApp.Data.Repository;
using NetCore3._0Learn.WebApp.Data.Service;
using NetCore3._0Learn.WebApp.Models;

namespace NetCore3._0Learn.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IBlogService _blogService;

        public HomeController(ILogger<HomeController> logger, IBlogService blogService)
        {
            _logger = logger;
            _blogService = blogService;
        }

        public IActionResult Index()
        { 
            _blogService.Add();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}