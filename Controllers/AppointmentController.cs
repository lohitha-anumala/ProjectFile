using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMVC.Models;
using System.Data.Entity;
using ProjectMVC.Models.ViewModel;

namespace ProjectMVC.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        private ApplicationDbContext _context;

        public AppointmentController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpGet]
        public ActionResult Index()
        {
            var appointment = _context.Appointments.Include(c => c.Product).ToList();

            return View(appointment);

            
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Create(Appointment appointment)
        {

            var create = _context.Products.ToList();

            //if (updateC == null)
            //{
            //    return HttpNotFound();
            //}
            var vm = new NewAppointmentViewModel
            {
                // Appointment = updateCust,
                Product = create,
            };
            _context.Appointments.Add(appointment);
            return RedirectToAction("Index");
       
          

            //return View(vm);

        } 


        public ActionResult New()
        {
            var product1 = _context.Products.ToList();
            var viewModel = new NewAppointmentViewModel
            {
                Product=product1
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Edit(int id)
        {
            var updateCust = _context.Appointments.SingleOrDefault(c => c.Id == id);
            var edit = _context.Products.ToList();


            if (updateCust == null)
           {
                return HttpNotFound();
            }
            var vm = new NewAppointmentViewModel
            {
                Appointment = updateCust,
                Product = edit
            };
            return View(vm);
        }
        public ActionResult Save(Appointment appointment)
        {
            if (appointment.AId == 0)
                _context.Appointments.Add(appointment);
            else
            {
                var AppointmentInDb = _context.Appointments.Single(c => c.Id == appointment.AId);
                AppointmentInDb.PersonName = appointment.PersonName;
                AppointmentInDb.PhoneNumber = appointment.PhoneNumber;
                AppointmentInDb.Email = appointment.Email;
                AppointmentInDb.Date = appointment.Date;
                AppointmentInDb.Product = appointment.Product;
                    
                
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Appointment");//Back to customer controller page
        }
       
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}