using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_sem2.Models;
using Project_sem2.Service;

namespace Project_sem2.Controllers
{
    public class MassageController : Controller
    {
        public MassageService service;

        public MassageController(MassageService service_)
        {
            service = service_;
        }
        public IActionResult Index()
        {

            return View(service.ReadMassage());
        }

        [HttpGet]//httpget means get request and http post means post request  
        public IActionResult AddMassage()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddMassage(Massage newMassage)
        {

            service.AddMassage(newMassage);
            return RedirectToAction("Index");
        
        }

        [HttpGet]
        public IActionResult EditMassage(int id)
        {
            var result = service.context.massageList.FirstOrDefault(c => c.Id == id);
            return View(result);
        }

        [HttpPost]
        public IActionResult EditMassage(Massage m)
        {
            service.EditMassage(m);
            return RedirectToAction("Index");

        }
        

        //delete
        [HttpPost]
        public IActionResult DeleteMassage(Massage m)
        {
            service.DeleteMassage(m);
            
            return RedirectToAction("Index");
            // return Json(new { success = true });
        }
    }

    
}
