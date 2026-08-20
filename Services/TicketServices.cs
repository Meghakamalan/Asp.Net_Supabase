using TicketTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace TicketTracker.Services
{
    // C# Model representing a map marker location
    public class MapPoint
    {
        public string CityName { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Label { get; set; } = string.Empty; // e.g. "Departure", "Layover 1", "Destination"
    }

    public class TicketServices
    {
        public TicketDbContext context;

        public TicketServices(TicketDbContext context_)
        {
            context = context_;
        }

        public List<Town> GetAllTowns()
        {
            return context.townList.ToList();
        }

        public Ticket GetCheapestTicket(int fromTownId, int toTownId)
        {
            return context.ticketList.Include(t => t.FromTown)
            .Include(t => t.ToTown)
            .Where(t => t.FromTownId == fromTownId && t.ToTownId == toTownId)
            .OrderBy(t => t.Price)
            .FirstOrDefault();
        }

        public RouteResult FindCheapestRoute(int fromTownId, int toTownId)
        {
            var allTickets = context.ticketList
                .Include(t => t.FromTown)
                .Include(t => t.ToTown)
                .ToList();

            var allTowns = context.townList.ToList();

            var cheapestCost = new Dictionary<int, decimal>();
            var bestPath = new Dictionary<int, List<Ticket>>();
            var unvisited = new HashSet<int>(allTowns.Select(t => t.Id));

            foreach (var town in allTowns)
            {
                cheapestCost[town.Id] = decimal.MaxValue;
                bestPath[town.Id] = new List<Ticket>();
            }
            cheapestCost[fromTownId] = 0;

            while (unvisited.Count > 0)
            {
                var reachableUnvisited = unvisited
                    .Where(id => cheapestCost[id] != decimal.MaxValue)
                    .OrderBy(id => cheapestCost[id])
                    .ToList();

                if (!reachableUnvisited.Any()) break;

                int currentTownId = reachableUnvisited.First();
                if (currentTownId == toTownId) break;

                unvisited.Remove(currentTownId);

                var ticketsFromHere = allTickets
                    .Where(t => t.FromTownId == currentTownId)
                    .ToList();

                foreach (var ticket in ticketsFromHere)
                {
                    int neighbourId = ticket.ToTownId;
                    decimal newCost = cheapestCost[currentTownId] + ticket.Price;

                    if (newCost < cheapestCost[neighbourId])
                    {
                        cheapestCost[neighbourId] = newCost;
                        bestPath[neighbourId] = new List<Ticket>(bestPath[currentTownId]) { ticket };
                    }
                }
            }

            if (cheapestCost[toTownId] == decimal.MaxValue)
                return null;

            var resultTickets = bestPath[toTownId];

            return new RouteResult
            {
                Tickets = resultTickets,
                TotalPrice = cheapestCost[toTownId],
                TotalDurationHours = resultTickets.Sum(t => t.DurationHours)
            };
        }

        // C# method that builds a List of MapPoint objects
        public List<MapPoint> GetRouteMapPoints(RouteResult routeResult)
        {
            var points = new List<MapPoint>();

            if (routeResult == null || routeResult.Tickets == null || !routeResult.Tickets.Any())
            {
                return points;
            }

            // 1. Add Departure Point
            var firstTicket = routeResult.Tickets.First();
            points.Add(new MapPoint
            {
                CityName = firstTicket.FromTown.Name,
                Latitude = firstTicket.FromTown.Latitude,
                Longitude = firstTicket.FromTown.Longitude,
                Label = "Departure"
            });

            // 2. Add Layovers and Destination
            for (int i = 0; i < routeResult.Tickets.Count; i++)
            {
                var ticket = routeResult.Tickets[i];
                bool isDestination = (i == routeResult.Tickets.Count - 1);

                points.Add(new MapPoint
                {
                    CityName = ticket.ToTown.Name,
                    Latitude = ticket.ToTown.Latitude,
                    Longitude = ticket.ToTown.Longitude,
                    Label = isDestination ? "Destination" : $"Layover {i + 1}"
                });
            }

            return points;
        }
    }
}