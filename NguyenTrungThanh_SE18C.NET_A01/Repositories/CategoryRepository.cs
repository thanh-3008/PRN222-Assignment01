using BusinessObjects;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FUNewsManagementDbContext _context;

        public CategoryRepository(FUNewsManagementDbContext context)
        {
            _context = context;
        }

        public bool IsCategoryInUse(short categoryId)
        {
            return _context.NewsArticles.Any(n => n.CategoryId == categoryId);
        }

        public List<Category> GetCategories(string? searchTerm = null)
        {
            var query = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.CategoryName.Contains(searchTerm));
            }
            return query.ToList();
        }

        public void SaveCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public Category? GetCategoryById(short id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}