using System.ComponentModel.DataAnnotations;

namespace TicketTracker.Models
{
    public class Town
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}