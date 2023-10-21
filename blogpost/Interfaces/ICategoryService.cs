using blogpost.Dto;
using blogpost.Models;

namespace blogpost.Interfaces
{
    public interface ICategoryService
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int categoryId);
        ICollection<BlogPost> GetBlogPostByCategory(int categoryId);
        bool CategoryExist(int categoryId);
        bool CreateCategory(string categoryName);
        bool UpdateCategory(Category category);
        bool DeleteCategory(int categoryId);
        bool Save();
    }
}
