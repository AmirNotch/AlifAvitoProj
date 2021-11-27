using AlifAvitoProj.Context;
using AlifAvitoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private AvitoDbContext _categoryContext;

        public CategoryRepository(AvitoDbContext categoryContext)
        {
            _categoryContext = categoryContext;
        }

        public bool CategoryExists(string categoryName)
        {
            return _categoryContext.Categories.Any(c => c.Name == categoryName);
        }

        public bool CategoryExistsById(int categoryId)
        {
            return _categoryContext.Categories.Any(c => c.Id == categoryId);
        }

        public bool CreateCategory(Category category)
        {
            _categoryContext.AddAsync(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _categoryContext.Remove(category);
            return Save();
        }

        public ICollection<Advert> GetAllAdvertsForCategory(int categoryId)
        {
            return _categoryContext.AdvertCategories.Where(c => c.CategoryId == categoryId).Select(b => b.Advert).ToList();
        }

        public ICollection<Category> GetAllCategoriesForAdvert(int advertId)
        {
            return _categoryContext.AdvertCategories.Where(b => b.AdvertId == advertId).Select(c => c.Category).ToList();
        }

        public ICollection<Category> GetCategories()
        {
            return _categoryContext.Categories.OrderBy(c => c.Name).ToList();
        }

        public Category GetCategory(string categoryName)
        {
            return _categoryContext.Categories.Where(c => c.Name.Contains(categoryName)).FirstOrDefault();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _categoryContext.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
        }

        public bool IsDuplicateCategoryById(int categoryId, string categoryName)
        {
            var category = _categoryContext.Categories.Where(c => c.Name.Trim().ToUpper() == categoryName.Trim().ToUpper()
                                                && c.Id != categoryId).FirstOrDefault();
            return category == null ? false : true;
        }

        public bool IsDuplicateCategoryName(string categoryId, string categoryName)
        {
            var category = _categoryContext.Categories.Where(c => c.Name.Trim().ToUpper() == categoryName.Trim().ToUpper()).FirstOrDefault();
            return category == null ? false : true;
        }

        public bool Save()
        {
            var save = _categoryContext.SaveChanges();
            return save >= 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _categoryContext.Update(category);
            return Save();
        }
    }
}
