using Microsoft.EntityFrameworkCore;
using TicketTracker.Models;

namespace TicketTracker.Services
{
    // Represents a location that can be displayed on the map
    public class MapPoint
    {
        public string CityName { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Label { get; set; } = string.Empty;
    }

    public class TicketServices
    {
        private readonly TicketDbContext _context;

        public TicketServices(TicketDbContext context)
        {
            _context = context;
        }

        // =========================================================
        // TOWN OPERATIONS - READ
        // =========================================================

        public List<Town> GetAllTowns()
        {
            return _context.Towns
                .OrderBy(t => t.Name)
                .ToList();
        }

        // =========================================================
        // TICKET CRUD
        // =========================================================

        // READ - Get all tickets
        public List<Ticket> GetAllTickets()
        {
            return _context.Tickets
                .Include(t => t.FromTown)
                .Include(t => t.ToTown)
                .OrderBy(t => t.FromTown.Name)
                .ThenBy(t => t.ToTown.Name)
                .ToList();
        }

        // READ - Get one ticket
        public Ticket GetTicketById(int id)
        {
            return _context.Tickets
                .AsNoTracking() // Prevents EF Core from tracking this instance
                .Include(t => t.FromTown)
                .Include(t => t.ToTown)
                .FirstOrDefault(t => t.Id == id);
        }

        // CREATE
        public void AddTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
        }

        // UPDATE
        public void UpdateTicket(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            _context.SaveChanges();
        }

        // DELETE
        public bool DeleteTicket(int id)
        {
            var ticket = _context.Tickets
                .FirstOrDefault(t => t.Id == id);

            if (ticket == null)
            {
                return false;
            }

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();

            return true;
        }

        // =========================================================
        // SEARCH / CHEAPEST TICKET
        // =========================================================

        public Ticket GetCheapestTicket(
            int fromTownId,
            int toTownId)
        {
            return _context.Tickets
                .Include(t => t.FromTown)
                .Include(t => t.ToTown)
                .Where(t =>
                    t.FromTownId == fromTownId &&
                    t.ToTownId == toTownId)
                .OrderBy(t => t.Price)
                .FirstOrDefault();
        }

        // =========================================================
        // CHEAPEST ROUTE
        // =========================================================

        public RouteResult FindCheapestRoute(
            int fromTownId,
            int toTownId)
        {
            var allTickets = _context.Tickets
                .Include(t => t.FromTown)
                .Include(t => t.ToTown)
                .ToList();

            var allTowns = _context.Towns.ToList();

            var cheapestCost =
                new Dictionary<int, decimal>();

            var bestPath =
                new Dictionary<int, List<Ticket>>();

            var unvisited =
                new HashSet<int>(
                    allTowns.Select(t => t.Id)
                );

            foreach (var town in allTowns)
            {
                cheapestCost[town.Id] =
                    decimal.MaxValue;

                bestPath[town.Id] =
                    new List<Ticket>();
            }

            // Starting town costs nothing
            cheapestCost[fromTownId] = 0;

            while (unvisited.Count > 0)
            {
                var reachableUnvisited = unvisited
                    .Where(id =>
                        cheapestCost[id] != decimal.MaxValue)
                    .OrderBy(id => cheapestCost[id])
                    .ToList();

                if (!reachableUnvisited.Any())
                {
                    break;
                }

                int currentTownId =
                    reachableUnvisited.First();

                if (currentTownId == toTownId)
                {
                    break;
                }

                unvisited.Remove(currentTownId);

                var ticketsFromHere = allTickets
                    .Where(t =>
                        t.FromTownId == currentTownId)
                    .ToList();

                foreach (var ticket in ticketsFromHere)
                {
                    int neighbourId =
                        ticket.ToTownId;

                    decimal newCost =
                        cheapestCost[currentTownId]
                        + ticket.Price;

                    if (newCost <
                        cheapestCost[neighbourId])
                    {
                        cheapestCost[neighbourId] =
                            newCost;

                        bestPath[neighbourId] =
                            new List<Ticket>(
                                bestPath[currentTownId]
                            )
                            {
                                ticket
                            };
                    }
                }
            }

            // No route found
            if (cheapestCost[toTownId] ==
                decimal.MaxValue)
            {
                return null;
            }

            var resultTickets =
                bestPath[toTownId];

            return new RouteResult
            {
                Tickets = resultTickets,

                TotalPrice =
                    cheapestCost[toTownId],

                TotalDurationHours =
                    resultTickets.Sum(
                        t => t.DurationHours)
            };
        }

        // =========================================================
        // MAP POINTS
        // =========================================================

        public List<MapPoint> GetRouteMapPoints(
            RouteResult routeResult)
        {
            var points = new List<MapPoint>();

            if (routeResult == null ||
                routeResult.Tickets == null ||
                !routeResult.Tickets.Any())
            {
                return points;
            }

            // Departure
            var firstTicket =
                routeResult.Tickets.First();

            points.Add(new MapPoint
            {
                CityName =
                    firstTicket.FromTown.Name,

                Latitude =
                    firstTicket.FromTown.Latitude,

                Longitude =
                    firstTicket.FromTown.Longitude,

                Label = "Departure"
            });

            // Layovers and destination
            for (
                int i = 0;
                i < routeResult.Tickets.Count;
                i++)
            {
                var ticket =
                    routeResult.Tickets[i];

                bool isDestination =
                    i ==
                    routeResult.Tickets.Count - 1;

                points.Add(new MapPoint
                {
                    CityName =
                        ticket.ToTown.Name,

                    Latitude =
                        ticket.ToTown.Latitude,

                    Longitude =
                        ticket.ToTown.Longitude,

                    Label =
                        isDestination
                            ? "Destination"
                            : $"Layover {i + 1}"
                });
            }

            return points;
        }
    }
}