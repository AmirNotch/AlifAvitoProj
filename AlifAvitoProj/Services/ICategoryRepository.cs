using AlifAvitoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Services
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(string categoryName);
        Category GetCategoryById(int categoryId);
        ICollection<Advert> GetAllAdvertsForCategory(int categoryId);
        ICollection<Category> GetAllCategoriesForAdvert(int advertId);
        bool CategoryExists(string categoryName);
        bool CategoryExistsById(int categoryId);
        bool IsDuplicateCategoryName(string categoryName, string categoryClassName);
        bool IsDuplicateCategoryById(int categoryId, string categoryName);

        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}
