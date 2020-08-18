using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CampChetek.Models;
using System.Net.Mail;

namespace CampChetek.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private Persons context { get; set; }
        public HomeController(Persons ctx)
        {
            context = ctx;
        }

        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Action = "Submit";
            ViewBag.Bedding = context.beddings.OrderBy(b => b.Name).ToList();
            ViewBag.Reason = context.Reasons.OrderBy(r => r.Name).ToList();
            return View(new Events());
        }

        [HttpPost]
        public IActionResult Index(Events housing)
        {
            housing.title = housing.FirstName.ToUpper() + " " + housing.LastName.ToUpper() + " | Room: " + context.Room.Find(housing.RoomsId).Name.ToString();
            housing.description = housing.FirstName.ToUpper() + " " + housing.LastName.ToUpper() + " | Bedding: " + context.beddings.Find(housing.BeddingId).Name.ToString() + " | Number Of Guest: " + housing.NumberGuest.ToString();
            context.events.Add(housing);
            context.SaveChanges();

            string bcc = "danael@campchetek.org";
            string to = housing.Email;
            string subject = "Thank You For Your Housing Request";
            string boby = "Thank You " + housing.FirstName + " we will get back to you soon about staying.<br><br> " + "Dates Requested: " + housing.event_start.ToString("MM/dd/yyyy") + "-" + housing.event_end.ToString("MM/dd/yyyy") + ". ";
            MailMessage mm = new MailMessage();
            mm.To.Add(to);
            mm.Subject = subject;
            mm.Body = boby;
            mm.From = new MailAddress("dannykluver1@gmail.com");
            mm.Bcc.Add(bcc);
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("dannykluver1@gmail.com", "HideMenu45");
            smtp.Send(mm);
            ViewBag.message = "The Mail Has Been Sent To " + housing.Email + " Successfully..!";
            
            return RedirectToAction("ThankYou", "Home");
        }

        public IActionResult ThankYou()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
