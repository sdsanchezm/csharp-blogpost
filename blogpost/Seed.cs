using blogpost.Models;
using blogpost.Data;


namespace blogpost
{
    public class Seed
    {
        private readonly DataContext _dbContext;

        public Seed(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedDataContext()
        {
            if (!_dbContext.BlogPostPostauthors_dbs.Any())
            {
                var blogPostAuthor1 = new List<BlogPostPostauthor>()
                {
                    new BlogPostPostauthor()
                    {
                       BlogPostJT = new BlogPost()
                        {
                            Title = "Cars",
                            CreationDate = DateTime.Now,
                            PostCategories = new List<PostCategory>()
                            {
                                new PostCategory { CategoryJT = new Category() { CategoryName = "Category 1" } }
                            },
                            PostComments = new List<Comment>()
                            {
                                new Comment() { CommentTitle = "comment 1 title here", CommentContent = "this is a comment content", Rate = 2, Commenter = new CommentAuthor(){ Username = "user1" } },
                                new Comment() { CommentTitle = "comment 2 title here", CommentContent = "this is the second comment content", Rate = 3, Commenter = new CommentAuthor(){ Username = "user2" } }
                            }
                        },
                       PostAuthorJT = new PostAuthor()
                       {
                           AuthorUsername = "Jamecho",
                           FavLanguage = "Python",
                           AuthorPostCity = new City() { CityName = "Chupamestepenco" }
                       }
                    },
                    new BlogPostPostauthor()
                    {
                       BlogPostJT = new BlogPost()
                        {
                            Title = "Cars",
                            CreationDate = DateTime.Now,
                            PostCategories = new List<PostCategory>()
                            {
                                new PostCategory { CategoryJT = new Category() { CategoryName = "Category 2" } }
                            },
                            PostComments = new List<Comment>()
                            {
                                new Comment() { CommentTitle = "comment 21 title here", CommentContent = "this is a comment content", Rate = 2, Commenter = new CommentAuthor(){ Username = "user6" } },
                                new Comment() { CommentTitle = "comment 22 title here", CommentContent = "this is the second comment content", Rate = 3, Commenter = new CommentAuthor(){ Username = "user7" } }
                            }
                        },
                       PostAuthorJT = new PostAuthor()
                       {
                           AuthorUsername = "Jara",
                           FavLanguage = "Java",
                           AuthorPostCity = new City() { CityName = "Trinaseco" }
                       }
                    },
                    new BlogPostPostauthor()
                    {
                       BlogPostJT = new BlogPost()
                        {
                            Title = "Cars",
                            CreationDate = DateTime.Now,
                            PostCategories = new List<PostCategory>()
                            {
                                new PostCategory { CategoryJT = new Category() { CategoryName = "Category 3" } }
                            },
                            PostComments = new List<Comment>()
                            {
                                new Comment() { CommentTitle = "comment 31 title here", CommentContent = "this is a comment content", Rate = 2, Commenter = new CommentAuthor(){ Username = "user3" } },
                                new Comment() { CommentTitle = "comment 32 title here", CommentContent = "this is the second comment content", Rate = 3, Commenter = new CommentAuthor(){ Username = "user4" } }
                            }
                        },
                       PostAuthorJT = new PostAuthor()
                       {
                           AuthorUsername = "Tiche",
                           FavLanguage = "Dotnet",
                           AuthorPostCity = new City() { CityName = "Bulbamelgar" }
                       }
                    }
                };

                _dbContext.BlogPostPostauthors_dbs.AddRange(blogPostAuthor1);
                _dbContext.SaveChanges();

            }
        }
    }
}
