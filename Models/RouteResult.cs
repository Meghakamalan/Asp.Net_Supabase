namespace TicketTracker.Models
{
    public class RouteResult
    {
        public List<Ticket> Tickets { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalDurationHours { get; set; }
        public bool IsDirect => Tickets.Count == 1;
        public string Summary => string.Join(" → ",
            Tickets.Select(t => t.FromTown.Name)
            .Concat(new[] { Tickets.Last().ToTown.Name }));
    }
}