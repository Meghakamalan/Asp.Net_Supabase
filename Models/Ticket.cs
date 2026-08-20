using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TicketTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a departure town.")]
        public int FromTownId { get; set; }

        [ValidateNever]
        public Town? FromTown { get; set; }

        [Required(ErrorMessage = "Please select a destination town.")]
        public int ToTownId { get; set; }

        [ValidateNever]
        public Town? ToTown { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Airline name is required.")]
        [StringLength(100)]
        public string Airline { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "Duration must be between 1 and 100 hours.")]
        public int DurationHours { get; set; }
    }
}