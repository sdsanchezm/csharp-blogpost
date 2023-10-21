using blogpost.Data;
using blogpost.Interfaces;
using blogpost.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace blogpost.Services
{
    public class BlogPostService : IBlogPostService
    {
        DataContext _dataContext;
        public BlogPostService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<BlogPost> GetBlogPosts()
        {
            return _dataContext.BlogPosts_dbs.OrderBy(p => p.Id).ToList();
        }

        public BlogPost GetBlogPost(int blogPostId)
        {
            var bp = _dataContext.BlogPosts_dbs.Where(p => p.Id == blogPostId).FirstOrDefault();
            return bp;
        }

        public BlogPost GetBlogPost(string blogPostTitle)
        {
            var bp = _dataContext.BlogPosts_dbs.Where(p => p.Title == blogPostTitle).FirstOrDefault();
            return bp;
        }

        public decimal GetBlogPostAverageRate(int blogPostId)
        {
            var comment = _dataContext.Comments_dbs.Where(p => p.BlogPost.Id == blogPostId);

            if (comment.Count() <= 0)
                return 0;

            var average = ((decimal)comment.Sum(p => p.Rate) / comment.Count());

            return average;
        }

        public bool BlogPostExists(int blogPostId)
        {
            var bp = _dataContext.BlogPosts_dbs.Any(p => p.Id == blogPostId);
            return bp;
        }

        public bool CreateBlogPost(int authorId, int categoryId, BlogPost blogPost)
        {
            var a = _dataContext.PostAuthors_dbs.Where(p => p.Id == authorId).FirstOrDefault();

            var blogPostPostauthorLocal = new BlogPostPostauthor
            {
                PostAuthorJT = a,
                BlogPostJT = blogPost,
            };

            var c = _dataContext.Categories_dbs.Where(p => p.Id == categoryId).FirstOrDefault();

            var postCategoryLocal = new PostCategory
            {
                CategoryJT = c,
                BlogPostJT = blogPost,
            };

            _dataContext.Add(postCategoryLocal);
            _dataContext.Add(blogPostPostauthorLocal);
            _dataContext.Add(blogPost);

            return Save();

        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBlogPost(int authorId, int categoryId, BlogPost blogPost)
        {
            //var bpLocal = _dataContext.BlogPosts_dbs.Where(p => p.Id == blogPost.Id).FirstOrDefault();
            //if (bpLocal == null)
            //    return false;

            var categorylocal = _dataContext.Categories_dbs.Where(p => p.Id == categoryId).FirstOrDefault();
            if (categorylocal ==  null)
                return false;

            var authorLocal = _dataContext.PostAuthors_dbs.Where(p => p.Id == authorId).FirstOrDefault();
            if (authorLocal == null)
                return false;

            // blogPost.Title = blogPost.Title;
            blogPost.PostCategories = blogPost.PostCategories;
            blogPost.BlogPostPostauthors = blogPost.BlogPostPostauthors;

            _dataContext.Update(blogPost);
            return Save();
        }

        public bool DeleteBlogPost(int blogPostId)
        {
            var bp = _dataContext.BlogPosts_dbs.Where(p => p.Id == blogPostId).FirstOrDefault();
            _dataContext.Remove(bp);
            return Save();
        }
    }
}
