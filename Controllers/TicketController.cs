using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketTracker.Models;
using TicketTracker.Services;

namespace TicketTracker.Controllers
{
    public class TicketController : Controller
    {
        public TicketServices service;//this is a service class

        public TicketController(TicketServices service_) // ASP.NET injects the service directly
        {
            service = service_;
        }

        public IActionResult Index()
        {
            Console.WriteLine("Index loaded");
            ViewBag.Towns = new SelectList(service.GetAllTowns(), "Id", "Name");
            return View();
        }

        // GET: /Ticket/SearchTicket — shows the search form page
        public IActionResult SearchTicket()
        {
            ViewBag.Towns = new SelectList(service.GetAllTowns(), "Id", "Name");
            return View();
        }

        // POST: /Ticket/FindCheapest — receives the two towns and finds cheapest ticket
        [HttpPost]
        public IActionResult FindCheapest(int fromTownId, int toTownId)
        {
            ViewBag.Towns = new SelectList(service.GetAllTowns(), "Id", "Name");

            if (fromTownId == toTownId)
            {
                ViewBag.Error = "Please select two different towns.";
                return View("Index");
            }

            var route = service.FindCheapestRoute(fromTownId, toTownId);

            if (route == null)
            {
                ViewBag.Error = "No route found between these towns.";
                return View("Index");
            }

            // Call service method to get C# MapPoint list
            ViewBag.MapPoints = service.GetRouteMapPoints(route);

            return View("Result", route);
        }
    }
}