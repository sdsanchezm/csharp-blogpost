﻿using blogpost.Interfaces;
using blogpost.Models;
using blogpost.Data;

namespace blogpost.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dbContext;

        public CategoryService(DataContext categoryContext)
        {
            _dbContext = categoryContext;
        }

        public bool CategoryExist(int categoryId)
        {
            return _dbContext.Categories_dbs.Any(p => p.Id == categoryId);
        }

        public ICollection<BlogPost> GetBlogPostByCategory(int categoryId)
        {
            var bp = _dbContext.PostCategories_dbs.Where(p => p.CategoryId == categoryId).Select(b => b.BlogPostJT).ToList();
            return bp;
        }

        public Category GetCategory(int categoryId)
        {
            var c = _dbContext.Categories_dbs.Where(p => p.Id == categoryId).FirstOrDefault();
            return c;
        }

        public ICollection<Category> GetCategories()
        {
            return _dbContext.Categories_dbs.ToList();
        }
    }
}