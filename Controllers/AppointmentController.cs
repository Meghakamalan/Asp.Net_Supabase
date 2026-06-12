using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_sem2.Models;
using Project_sem2.Service;

namespace Project_sem2.Controllers
{
    public class AppointmentController : Controller
    {
        public AppointmentService service;

        public AppointmentController(AppointmentService service_)
        {
            service = service_;
        }
        public IActionResult Index()
        {

            return View(service.ReadClient());
        }

        [HttpGet]//httpget means get request and http post means post request  
        public IActionResult AddClient()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddClient(Client newClient)
        {

            service.AddClient(newClient);
            return RedirectToAction("Index");
        
        }

        [HttpGet]
        public IActionResult EditClient(int id)
        {
            var result = service.context.clientList.FirstOrDefault(c => c.Id == id);
            return View(result);
        }

        [HttpPost]
        public IActionResult EditClient(Client newClient)
        {
            service.EditClient(newClient);
            return RedirectToAction("Index");

        }
        

        //delete
        [HttpPost]
        public IActionResult DeleteClient(Client c)
        {
            service.DeleteClient(c);
            
            return RedirectToAction("Index");
            // return Json(new { success = true });
        }
    }

    
}
