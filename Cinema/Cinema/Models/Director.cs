using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class Director
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string Surname { get; set; }
        public ICollection<Movie>? Movies { get; set; }
    }
}
