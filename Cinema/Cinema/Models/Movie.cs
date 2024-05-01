using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Required]
        public string Title { get; set; }

        [Range(1, int.MaxValue)]
        public int Duration { get; set; }
        [Display(Name = "Director")]
        public int DirectorId { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Director? Director { get; set; }
        public Category? Category { get; set; }

        public ICollection<Showing>? Showings { get; set; }

    }
}
