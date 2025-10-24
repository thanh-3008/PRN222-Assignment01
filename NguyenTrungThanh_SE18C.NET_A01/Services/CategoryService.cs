using BusinessObjects;
using Repositories.Interfaces;
using Services.Interfaces;
using System.Collections.Generic;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public bool IsCategoryInUse(short categoryId)
        {
            return _categoryRepository.IsCategoryInUse(categoryId);
        }

        public List<Category> GetCategories(string? searchTerm = null)
        {
            return _categoryRepository.GetCategories(searchTerm);
        }

        public void SaveCategory(Category category)
        {
            _categoryRepository.SaveCategory(category);
        }

        public Category? GetCategoryById(short id)
        {
            return _categoryRepository.GetCategoryById(id);
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.UpdateCategory(category);
        }

        public void DeleteCategory(Category category)
        {
            _categoryRepository.DeleteCategory(category);
        }
    }
}