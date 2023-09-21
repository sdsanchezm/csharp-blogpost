namespace blogpost.Models
{
    public class CommentAuthor
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public virtual ICollection<Comment> Comments { get; set;}

    }

}