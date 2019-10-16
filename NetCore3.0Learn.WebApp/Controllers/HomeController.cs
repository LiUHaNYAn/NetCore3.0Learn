using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCore3._0Learn.WebApp.Data.Model;
using NetCore3._0Learn.WebApp.Data.Repository;
using NetCore3._0Learn.WebApp.Models;

namespace NetCore3._0Learn.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IRepository<Blog, Guid> _repository;
        private readonly IRepository<Post, Guid> postRepository;

        public HomeController(ILogger<HomeController> logger, IRepository<Blog, Guid> repository,
            IRepository<Post, Guid> postRepository)
        {
            _logger = logger;
            _repository = repository;
            this.postRepository = postRepository;
        }

        public IActionResult Index()
        {
//            _repository.Remove(Guid.Parse("BA196432-5285-4C28-A145-E2B5792E705C"));
            _repository.BeginTran();
            _repository.Remove(Guid.Parse("912693B0-5F61-42ED-B353-E79F0FD7E9E2"));
            var blogid = Guid.NewGuid();
//            postRepository.AddEntity(new Post {Id = Guid.NewGuid(), Title = "demo176FF941-7 ",BlogId = blogid});
            _repository.AddEntity(new Blog {Id = Guid.NewGuid(), CreateTime = DateTime.Now, Title = "你36995"});
            _repository.AddEntity(new Blog {Id = blogid, CreateTime = DateTime.Now, Title = "hellh  wo"});

            _repository.Commit();
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