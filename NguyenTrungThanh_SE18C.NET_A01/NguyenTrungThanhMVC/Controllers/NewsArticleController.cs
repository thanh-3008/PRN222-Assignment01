using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NguyenTrungThanhMVC.Models;
using Services.Interfaces;
using System.Linq;
using System.Security.Claims;

namespace NguyenTrungThanhMVC.Controllers
{
    [Authorize(Roles = "Staff")]
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public NewsArticleController(INewsArticleService newsArticleService, ICategoryService categoryService, ITagService tagService)
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public IActionResult Index()
        {
            var newsArticles = _newsArticleService.GetNewsArticles();
            return View(newsArticles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new NewsArticleViewModel
            {
                Categories = new SelectList(_categoryService.GetCategories().Where(c => c.IsActive), "CategoryId", "CategoryName"),
                AllTags = _tagService.GetTags()
            };
            return PartialView("_CreateNewsArticlePartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewsArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = new NewsArticle
                {
                    NewsArticleId = Guid.NewGuid().ToString(),
                    NewsTitle = model.NewsTitle,
                    Headline = model.Headline,
                    NewsContent = model.NewsContent,

                    NewsStatus = true,

                    CategoryId = model.CategoryId,
                    CreatedDate = DateTime.Now,
                    CreatedById = short.Parse(User.FindFirstValue("AccountId"))
                };

                if (model.SelectedTagIds != null)
                {
                    foreach (var tagId in model.SelectedTagIds)
                    {
                        article.NewsTags.Add(new NewsTag { TagId = tagId });
                    }
                }

                _newsArticleService.SaveNewsArticle(article);
                return Json(new { success = true });
            }

            model.Categories = new SelectList(_categoryService.GetCategories().Where(c => c.IsActive), "CategoryId", "CategoryName", model.CategoryId);
            model.AllTags = _tagService.GetTags();
            return PartialView("_CreateNewsArticlePartial", model);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (id == null) return NotFound();
            var article = _newsArticleService.GetNewsArticleById(id);
            if (article == null) return NotFound();

            var model = new NewsArticleViewModel
            {
                NewsArticleId = article.NewsArticleId,
                NewsTitle = article.NewsTitle,
                Headline = article.Headline, 
                NewsContent = article.NewsContent,
                NewsStatus = article.NewsStatus ?? false,
                CategoryId = (short)article.CategoryId,
                Categories = new SelectList(_categoryService.GetCategories().Where(c => c.IsActive), "CategoryId", "CategoryName", article.CategoryId),
                AllTags = _tagService.GetTags(),
                SelectedTagIds = article.NewsTags.Select(t => t.TagId).ToList()
            };

            return PartialView("_EditNewsArticlePartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, NewsArticleViewModel model)
        {
            if (id != model.NewsArticleId) return NotFound();

            if (ModelState.IsValid)
            {
                var article = _newsArticleService.GetNewsArticleById(id);
                if (article == null) return NotFound();

                article.NewsTitle = model.NewsTitle;
                article.Headline = model.Headline; 
                article.NewsContent = model.NewsContent;
                article.NewsStatus = model.NewsStatus;
                article.CategoryId = model.CategoryId;
                article.ModifiedDate = DateTime.Now;
                article.UpdatedById = short.Parse(User.FindFirstValue("AccountId"));

                article.NewsTags.Clear();
                if (model.SelectedTagIds != null)
                {
                    foreach (var tagId in model.SelectedTagIds)
                    {
                        article.NewsTags.Add(new NewsTag { TagId = tagId });
                    }
                }

                _newsArticleService.UpdateNewsArticle(article);
                return Json(new { success = true });
            }

            model.Categories = new SelectList(_categoryService.GetCategories().Where(c => c.IsActive), "CategoryId", "CategoryName", model.CategoryId);
            model.AllTags = _tagService.GetTags();
            return PartialView("_EditNewsArticlePartial", model);
        }

        public IActionResult Details(string id)
        {
            if (id == null) return NotFound();
            var newsArticle = _newsArticleService.GetNewsArticleById(id);
            if (newsArticle == null) return NotFound();
            return View(newsArticle);
        }

        public IActionResult Delete(string id)
        {
            if (id == null) return NotFound();
            var newsArticle = _newsArticleService.GetNewsArticleById(id);
            if (newsArticle == null) return NotFound();
            return View(newsArticle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var newsArticle = _newsArticleService.GetNewsArticleById(id);
            if (newsArticle != null)
            {
                _newsArticleService.DeleteNewsArticle(newsArticle);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult History()
        {
            var accountId = short.Parse(User.FindFirstValue("AccountId"));
            var articles = _newsArticleService.GetNewsArticlesByAccountId(accountId);
            return View(articles);
        }
    }
}