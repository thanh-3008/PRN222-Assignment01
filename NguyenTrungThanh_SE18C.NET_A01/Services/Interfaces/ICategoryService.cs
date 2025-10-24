using BusinessObjects;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        bool IsCategoryInUse(short categoryId);
        List<Category> GetCategories(string? searchTerm = null);
        void SaveCategory(Category category);
        Category? GetCategoryById(short id);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}