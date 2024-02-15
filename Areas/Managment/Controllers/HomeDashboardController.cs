using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Managment")]
    public class HomeDashboardController : Controller
    {

        public IUnitOfWork UnitOfWork { get; }
        public HomeDashboardController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        // GET: HomeController
        public ActionResult TodayAppointment()
        {
            return View("TodayAppointment", UnitOfWork.AppointmentRepository.GetAllAppointments());
        }
        public ActionResult AllAppointment()
        {
            return View("AllAppointment", UnitOfWork.AppointmentRepository.GetAllAppointments());
        }

        [HttpPost]
        public ActionResult AllAppointment(int id)
        {
            return View("AllAppointment", UnitOfWork.AppointmentRepository.GetAllAppointments().Where(app=>app.AppointmentState == (AppointmentStates)id));
        }

        [HttpPost]
        public ActionResult TodayAppointmentAll(int id)
        {
            return View("AllAppointment", UnitOfWork.AppointmentRepository.GetAllAppointments().Where(a=>a.AppointmentDate.ToString("yyyy-MM-dd") ==  DateTime.Now.ToString("yyyy-MM-dd")));
        }
        
        [HttpPost]
        public ActionResult ClearOldAppointments()
        {
            var oldApps = UnitOfWork.AppointmentRepository.GetAllAppointments().Where(a=>a.AppointmentDate.Year <= DateTime.Now.Year && a.AppointmentDate.Month <= DateTime.Now.Month && a.AppointmentDate.Day < DateTime.Now.Day && a.AppointmentState != AppointmentStates.treated && a.AppointmentState != AppointmentStates.canceled).ToList();
            
            if(oldApps.Count != 0)
            {
                foreach (var app in oldApps)
                {
                    app.IsCanceled = true;
                    app.AppointmentState = AppointmentStates.canceled;
                    UnitOfWork.AppointmentRepository.UpdateAppointment(app.AppointmentId, app);
                }
            }


            return RedirectToAction("AllAppointment");
        }



        /* // GET: HomeController/Details/5
         public ActionResult Details(int id)
         {
             return View();
         }

         // GET: HomeController/Create
         public ActionResult Create()
         {
             return View();
         }

         // POST: HomeController/Create
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Create(IFormCollection collection)
         {
             try
             {
                 return RedirectToAction(nameof(Index));
             }
             catch
             {
                 return View();
             }
         }

         // GET: HomeController/Edit/5
         public ActionResult Edit(int id)
         {
             return View();
         }

         // POST: HomeController/Edit/5
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Edit(int id, IFormCollection collection)
         {
             try
             {
                 return RedirectToAction(nameof(Index));
             }
             catch
             {
                 return View();
             }
         }

         // GET: HomeController/Delete/5
         public ActionResult Delete(int id)
         {
             return View();
         }

         // POST: HomeController/Delete/5
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Delete(int id, IFormCollection collection)
         {
             try
             {
                 return RedirectToAction(nameof(Index));
             }
             catch
             {
                 return View();
             }
         }*/
    }
}
