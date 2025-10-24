using Microsoft.AspNetCore.Mvc;
using NguyenTrungThanhMVC.Models;
using Services.Interfaces;
using System.Diagnostics;
using System.Linq; 

namespace NguyenTrungThanhMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsArticleService _newsArticleService;

        public HomeController(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            var newsList = _newsArticleService.GetPublishedNewsArticles();
            return View(newsList);
        }


        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = _newsArticleService.GetNewsArticleById(id);
            if (newsArticle == null || newsArticle.NewsStatus != true)
            {
                return NotFound();
            }

            return View(newsArticle);
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