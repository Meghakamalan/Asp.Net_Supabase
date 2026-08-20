using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketTracker.Models;
using TicketTracker.Services;

namespace TicketTracker.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketServices _service;

        public TicketController(TicketServices service)
        {
            _service = service;
        }

        // GET: /Ticket/Index
        public IActionResult Index()
        {
            ViewBag.Towns =
                new SelectList(
                    _service.GetAllTowns(),
                    "Id",
                    "Name"
                );

            return View();
        }

        // GET: /Ticket/SearchTicket
        public IActionResult SearchTicket()
        {
            ViewBag.Towns =
                new SelectList(
                    _service.GetAllTowns(),
                    "Id",
                    "Name"
                );

            return View();
        }

        // POST: /Ticket/FindCheapest
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FindCheapest(
            int fromTownId,
            int toTownId)
        {
            ViewBag.Towns =
                new SelectList(
                    _service.GetAllTowns(),
                    "Id",
                    "Name"
                );

            // Validate different towns
            if (fromTownId == toTownId)
            {
                ViewBag.Error =
                    "Please select two different towns.";

                return View("Index");
            }

            // Find cheapest route
            var route =
                _service.FindCheapestRoute(
                    fromTownId,
                    toTownId
                );

            // No route found
            if (route == null)
            {
                ViewBag.Error =
                    "No route found between these towns.";

                return View("Index");
            }

            // Generate map points
            ViewBag.MapPoints =
                _service.GetRouteMapPoints(route);

            return View("Result", route);
        }
    }
}