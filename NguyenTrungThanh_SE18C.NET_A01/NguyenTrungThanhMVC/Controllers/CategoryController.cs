using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace NguyenTrungThanhMVC.Controllers
{
    [Authorize(Roles = "Staff")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetCategories();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_CreateCategoryPartial", new Category());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.SaveCategory(category);
                return Json(new { success = true });
            }
            return PartialView("_CreateCategoryPartial", category);
        }

        [HttpGet]
        public IActionResult Edit(short id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null) return NotFound();
            return PartialView("_EditCategoryPartial", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(short id, Category category)
        {
            if (id != category.CategoryId) return NotFound();

            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(category);
                return Json(new { success = true });
            }
            return PartialView("_EditCategoryPartial", category);
        }

        public IActionResult Details(short id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult Delete(short id)
        {
            if (_categoryService.IsCategoryInUse(id))
            {
                TempData["ErrorMessage"] = "Cannot delete this category because it is currently in use by one or more news articles.";
                return RedirectToAction(nameof(Index));
            }

            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(short id)
        {
            if (_categoryService.IsCategoryInUse(id))
            {
                TempData["ErrorMessage"] = "Cannot delete this category because it is currently in use.";
                return RedirectToAction(nameof(Index));
            }

            var category = _categoryService.GetCategoryById(id);
            if (category != null)
            {
                _categoryService.DeleteCategory(category);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}