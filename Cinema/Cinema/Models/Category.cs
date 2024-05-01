using System.ComponentModel.DataAnnotations;

namespace Cinema.Models

{
    public class Category
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
        public ICollection<Movie>? Movies { get; set; }
    }
}
