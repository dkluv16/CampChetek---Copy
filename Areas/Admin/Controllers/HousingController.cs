using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CampChetek.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using CampChetek.Library;
using CampChetek.DataAccessLayer;
using System.Net.Mail;

namespace CampChetek.Areas.Admin.Controllers
{
    [Route("[area]/[controller]/[action]")]
    [Area("Admin")]
    public class HousingController : Controller
    {
        private Persons context { get; set; }

        public HousingController(Persons ctx)
        {
            context = ctx;
        }

        [HttpGet]
        [Route("{orderby?}")]
        public IActionResult Dashboard(string orderby)
        {
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(orderby) ? "FirstNameDesc" : "";
            ViewBag.LastNameParm = orderby == "LastName" ? "lastNameDesc" : "LastName";
            ViewBag.RoomsIdParm = orderby == "RoomId" ? "roomIdDesc" : "RoomId";
            ViewBag.ArrivalDateParm = orderby == "ArrivalDate" ? "ArrivalDateDesc" : "ArrivalDate";
            ViewBag.DepartureDateParm = orderby == "DepartureDate" ? "DepartureDateDesc" : "DepartureDate";
            ViewBag.NumberGuestParm = orderby == "NumberGuest" ? "NumberGuestDesc" : "NumberGuest";
            ViewBag.BeddingParm = orderby == "Bedding" ? "BeddingDesc" : "Bedding";
            ViewBag.DateUpdateParm = orderby == "DateUpdate" ? "DateUpdateDesc" : "DateUpdate";

            var housing = from p in context.events select p;
            
            switch (orderby)
            {
                case "FirstNameDesc":
                    housing = housing.OrderByDescending(p => p.FirstName);
                    break;
                case "LastName":
                    housing = housing.OrderBy(s => s.LastName);
                    break;
                case "lastNameDesc":
                    housing = housing.OrderByDescending(p => p.LastName);
                    break;
                case "RoomId":
                    housing = housing.OrderBy(s => s.RoomsId);
                    break;
                case "roomIdDesc":
                    housing = housing.OrderByDescending(p => p.RoomsId);
                    break;
                case "ArrivalDate":
                    housing = housing.OrderBy(s => s.event_start);
                    break;
                case "ArrivalDateDesc":
                    housing = housing.OrderByDescending(p => p.event_start);
                    break;
                case "DepartureDate":
                    housing = housing.OrderBy(s => s.event_end);
                    break;
                case "DepartureDateDesc":
                    housing = housing.OrderByDescending(p => p.event_end);
                    break;
                case "NumberGuest":
                    housing = housing.OrderBy(s => s.NumberGuest);
                    break;
                case "NumberGuestDesc":
                    housing = housing.OrderByDescending(p => p.NumberGuest);
                    break;
                case "Bedding":
                    housing = housing.OrderBy(s => s.Bedding);
                    break;
                case "BeddingDesc":
                    housing = housing.OrderByDescending(p => p.Bedding);
                    break;
                case "DateUpdate":
                    housing = housing.OrderBy(s => s.DateUpdate);
                    break;
                case "DateUpdateDesc":
                    housing = housing.OrderByDescending(p => p.DateUpdate);
                    break;
                default:
                    housing = housing.OrderBy(s => s.FirstName);
                    break;
            }

            ViewBag.Bedding = context.beddings.OrderBy(b => b.Name).ToList();
            ViewBag.User = context.Users.OrderBy(u => u.FirstName).ToList();
            ViewBag.Rooms = context.Room.OrderBy(p => p.Name).ToList();
            var session = new HousingSession(HttpContext.Session);
            string check = session.GetName();

            if (check != null)
            {
                return View(housing.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult Detail(int id)
        {
            ViewBag.Bedding = context.beddings.OrderBy(b => b.Name).ToList();
            ViewBag.Rooms = context.Room.OrderBy(p => p.Name).ToList();
            ViewBag.User = context.Users.OrderBy(u => u.FirstName).ToList();
            ViewBag.User = context.Users.OrderBy(u => u.LastName).ToList();
            ViewBag.Reason = context.Reasons.OrderBy(r => r.Name).ToList();

            var housing = context.events.Find(id);
            return View(housing);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Bedding = context.beddings.OrderBy(b => b.Name).ToList();
            ViewBag.Rooms = context.Room.OrderBy(p => p.Name).ToList();
            ViewBag.User = context.Users.OrderBy(u => u.FirstName).ToList();
            ViewBag.User = context.Users.OrderBy(u => u.LastName).ToList();
            ViewBag.Reason = context.Reasons.OrderBy(r => r.Name).ToList();

            return View("Edit", new Models.Events());

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Bedding = context.beddings.OrderBy(b => b.Name).ToList();
            ViewBag.Rooms = context.Room.OrderBy(p => p.Name).ToList();
            ViewBag.User = context.Users.OrderBy(u => u.FirstName).ToList();
            ViewBag.User = context.Users.OrderBy(u => u.LastName).ToList();
            ViewBag.Reason = context.Reasons.OrderBy(r => r.Name).ToList();

            var housing = context.events.Find(id);

            return View(housing);
        }

        [HttpPost]
        public IActionResult Edit(Models.Events housing)
        {

            if (ModelState.IsValid)
            {
                if (housing.event_id == 0)
                {
                    housing.title = housing.FirstName + " " + housing.LastName + " | Room: " + context.Room.Find(housing.RoomsId).Name.ToString();
                    housing.description = housing.FirstName + " " + housing.LastName + " | Bedding: " + context.beddings.Find(housing.BeddingId).Name.ToString() + " | Number Of Guest: " + housing.NumberGuest.ToString();
                    context.events.Add(housing);

                    string bcc = "danael@campchetek.org";
                    string to = housing.Email;
                    string subject = "Thank You For Your Housing Request";
                    string boby = "Thank You " + housing.FirstName + " we will get back to you soon about staying<br><br> " + housing.event_start.ToString("MM/dd/yyyy") + "-" + housing.event_end.ToString("MM/dd/yyyy") + ". ";
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
                }
                else
                {
                    if (housing.sendEmail == true)
                    {
                        string bcc = "danael@campchetek.org";
                        string to = housing.Email;
                        string subject = "Camp Chetek Booking Confirmation";
                        string boby = housing.FirstName + " " + housing.LastName + "<br>" + housing.Address + "<br>" + housing.City + " " + housing.State + " " + housing.Zip + "<br><br>" + "Thank You " + housing.FirstName + ", for your recent request for accommodation.<br><br>" + "Below are the details of your booking. Please take a moment to check them, and if there are any problems, please call us so we can correct our records.<br><br>" + "We look forward to welcoming you, and will do our utmost to make your stay with us an enjoyable and memorable experience.<br><br><hr>" + "Arriving: " + housing.event_start.ToString("MM/dd/yyyy") + "<br>" + "Leaving: " + housing.event_end.ToString("MM/dd/yyyy") + "<br>" + "Guests: " + housing.NumberGuest.ToString() + "<br>" + "Room: " + context.Room.Find(housing.RoomsId).Name.ToString() + "<br>" + "Bedding: " + context.beddings.Find(housing.BeddingId).Name.ToString() + "<br><hr>";
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

                        housing.title = housing.FirstName.ToUpper() + " " + housing.LastName.ToUpper() + " | Room:" + context.Room.Find(housing.RoomsId).Name.ToString();
                        housing.description = housing.FirstName + " " + housing.LastName + "| Bedding:" + context.beddings.Find(housing.BeddingId).Name.ToString() + " | Number Of Guest: " + housing.NumberGuest.ToString();
                        housing.DateUpdate = DateTime.Now.ToString();
                        context.events.Update(housing);
                    }
                    else
                    {
                        housing.title = housing.FirstName.ToUpper() + " " + housing.LastName.ToUpper() + " | Room:" + context.Room.Find(housing.RoomsId).Name.ToString();
                        housing.description = housing.FirstName + " " + housing.LastName + "| Bedding:" + context.beddings.Find(housing.BeddingId).Name.ToString() + " | Number Of Guest: " + housing.NumberGuest.ToString();
                        housing.DateUpdate = DateTime.Now.ToString();
                        context.events.Update(housing);
                    }
                   
                }
                    
                context.SaveChanges();
                return RedirectToAction("Dashboard", "Housing");
            }   
            else
            {
                ViewBag.Action = (housing.event_id == 0) ? "Add" : "Edit";
                ViewBag.Bedding = context.beddings.OrderBy(b => b.Name).ToList();
                ViewBag.Rooms = context.Room.OrderBy(p => p.Name).ToList();
                ViewBag.User = context.Users.OrderBy(u => u.FirstName).ToList();
                ViewBag.User = context.Users.OrderBy(u => u.LastName).ToList();
                ViewBag.Reason = context.Reasons.OrderBy(r => r.Name).ToList();
                return View(housing);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Bedding = context.beddings.OrderBy(b => b.Name).ToList();
            ViewBag.Rooms = context.Room.OrderBy(p => p.Name).ToList();
            ViewBag.User = context.Users.OrderBy(u => u.FirstName).ToList();
            ViewBag.User = context.Users.OrderBy(u => u.LastName).ToList();
            ViewBag.Reason = context.Reasons.OrderBy(r => r.Name).ToList();
            var housing = context.events.Find(id);
            return View(housing);
        }

        [HttpPost]
        public IActionResult Delete(Models.Events housing)
        {
            if (housing.sendEmail == true)
            {
                string bcc = "danael@campchetek.org";
                string to = housing.Email.ToString();
                string subject = "Sorry To Inform You";
                string boby = housing.FirstName + " " + housing.LastName + "<br>" + housing.Address + "<br>" + housing.City + " " + housing.State + " " + "<br><br>" + "We're sorry to inform you overnight accommodations at Camp Chetek will not be available on " + housing.event_start.ToString("MM/dd/yyyy") + "-" + housing.event_end.ToString("MM/dd/yyyy") + " that you requested."+ "<br><br>" + "Have a blessed day<br><br>" + "Danael Kluver<br>" + "Office Manager";
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

                context.events.Remove(housing);
                context.SaveChanges();
                return RedirectToAction("Dashboard", "Housing");
            }
            else
            {
                context.events.Remove(housing);
                context.SaveChanges();
                return RedirectToAction("Dashboard", "Housing");
            }

        }

        public IActionResult RoomStatus()
        {
            
            var status = context.Room.Include(u => u.Status).OrderBy(u => u.Name).ToList();
           
            return View(status);
        }

        [HttpGet]
        public IActionResult EditStatus(int id)
        {
            ViewBag.Status = context.Statuses.OrderBy(u => u.Name).ToList();
            var housing = context.Room.Find(id);

            return View(housing);
        }

        [HttpPost]
        public IActionResult EditStatus(Rooms rooms)
        {
            ViewBag.Status = context.Statuses.OrderBy(u => u.Name).ToList();
            context.Room.Update(rooms);
            context.SaveChanges();
            return RedirectToAction("RoomStatus", "Housing");
        }

       
        public IActionResult NewSubmission(int check)
        {

            check = 30;
            var sub = from s in context.events select s;

            sub = sub.Where(s => s.RoomsId.Equals(check));

            ViewBag.Bedding = context.beddings.OrderBy(b => b.Name).ToList();
            ViewBag.Rooms = context.Room.OrderBy(p => p.Name).ToList();

            return View(sub.OrderBy(s => s.RoomsId).ToList());


        }

        public IActionResult Settings()
        {
            var users = from p in context.Users select p;

            var session = new HousingSession(HttpContext.Session);
            string check = session.GetName();

            if (check == "DANAEL@CAMPCHETEK.ORG")
            {
                return View(users.ToList());
            }
            else
            {
                return RedirectToAction("Dashboard", "Housing");
            }
            
        }

        [HttpGet]
        public IActionResult EditUsers(int id)
        {
            ViewBag.Useres = context.Users.OrderBy(u => u.FirstName).ToList();
            var user = context.Users.Find(id);

            return View(user);
        }

        [HttpPost]
        public IActionResult EditUsers(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
            return RedirectToAction("Dashboard", "Housing");
        }
    }
}