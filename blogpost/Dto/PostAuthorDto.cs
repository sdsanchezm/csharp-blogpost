using System.ComponentModel.DataAnnotations;

namespace blogpost.Dto
{
    public class PostAuthorDto
    {
        public int Id { get; set; }
        public string AuthorUsername { get; set; }
        public string FavLanguage { get; set; }

        [Range(1, 500, ErrorMessage = "CityId must be a positive integer.")]
        public int CityId { get; set; }
    }
}
