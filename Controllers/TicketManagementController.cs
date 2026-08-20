using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketTracker.Models;
using TicketTracker.Services;

namespace TicketTracker.Controllers
{
    public class TicketManagementController : Controller
    {
        private readonly TicketServices _service;

        public TicketManagementController(
            TicketServices service)
        {
            _service = service;
        }

        // =========================================================
        // READ
        // =========================================================

        // GET: /TicketManagement
        public IActionResult Index()
        {
            var tickets =
                _service.GetAllTickets();

            return View(tickets);
        }

        // =========================================================
        // CREATE
        // =========================================================

        // GET: /TicketManagement/Create
        [HttpGet]
        public IActionResult Create()
        {
            LoadTowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ticket ticket)
        {
            ValidateTicketRoute(ticket);

            if (!ModelState.IsValid)
            {
                // DEBUG: Output all errors to your console window
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { Field = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage) });

                foreach (var error in errors)
                {
                    Console.WriteLine($"[VALIDATION ERROR] Field: {error.Field}");
                    foreach (var msg in error.Errors)
                    {
                        Console.WriteLine($"   --> {msg}");
                    }
                }

                LoadTowns(ticket.FromTownId);
                return View(ticket);
            }

            _service.AddTicket(ticket);
            TempData["Success"] = "Ticket created successfully.";
            return RedirectToAction(nameof(Index));
        }
        // =========================================================
        // UPDATE
        // =========================================================

        // GET: /TicketManagement/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var ticket =
                _service.GetTicketById(id);

            if (ticket == null)
            {
                return NotFound();
            }

            LoadTowns(ticket.FromTownId);

            return View(ticket);
        }

        // POST: /TicketManagement/Edit
        // POST: /TicketManagement/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ticket ticket)
        {
            ValidateTicketRoute(ticket);

            if (!ModelState.IsValid)
            {
                LoadTowns(ticket.FromTownId);
                return View(ticket);
            }

            _service.UpdateTicket(ticket);

            TempData["Success"] = "Ticket updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // =========================================================
        // DELETE
        // =========================================================

        // GET: /TicketManagement/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var ticket =
                _service.GetTicketById(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: /TicketManagement/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var deleted =
                _service.DeleteTicket(id);

            if (!deleted)
            {
                return NotFound();
            }

            TempData["Success"] =
                "Ticket deleted successfully.";

            return RedirectToAction(nameof(Index));
        }

        // =========================================================
        // HELPER METHODS
        // =========================================================

        private void LoadTowns(int? selectedTownId = null)
        {
            ViewBag.Towns =
                new SelectList(
                    _service.GetAllTowns(),
                    "Id",
                    "Name",
                    selectedTownId
                );
        }

        private void ValidateTicketRoute(
            Ticket ticket)
        {
            if (ticket.FromTownId ==
                ticket.ToTownId)
            {
                ModelState.AddModelError(
                    "",
                    "Departure and destination must be different."
                );
            }

            if (ticket.Price <= 0)
            {
                ModelState.AddModelError(
                    "Price",
                    "Price must be greater than zero."
                );
            }

            if (ticket.DurationHours <= 0)
            {
                ModelState.AddModelError(
                    "DurationHours",
                    "Duration must be greater than zero."
                );
            }
        }
    }
}