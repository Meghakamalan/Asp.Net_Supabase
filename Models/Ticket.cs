namespace TicketTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int FromTownId { get; set; }
        public Town FromTown { get; set; }
        public int ToTownId { get; set; }
        public Town ToTown { get; set; }
        public decimal Price { get; set; }
        public string Airline { get; set; }      
        public int DurationHours { get; set; }   
    }
}