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
        bool CreateCategory(CategoryDto category);
        bool Save();
    }
}
