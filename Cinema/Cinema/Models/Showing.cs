using System.ComponentModel.DataAnnotations;
using Cinema.Validators;

namespace Cinema.Models
{
    public class Showing
    {
        public int Id { get; set; }

        [DateInFuture]
        [DataType(DataType.Date)]
        public DateTime Date {  get; set; }

        [DataType(DataType.Time)]
        [TimeInRange]
        public DateTime Time { get; set; }
        [Display(Name = "Movie")]
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}
