using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CampChetek.Models;

namespace CampChetek.Areas.Admin.Controllers
{
    [Route("[area]/[controller]/[action]")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private Persons context { get; set; }

        public HomeController(Persons ctx)
        {
            context = ctx;
        }

        [Route("{UserEmail?}")]
        [Route("{UserPassword}")]
        public IActionResult Index(string UserEmail, string UserPassword)
        {
            List<User> users = context.Users.Where(u => u.Email == UserEmail).ToList();
            List<User> usersPass = context.Users.Where(u => u.Password == UserPassword).ToList();

            if (users.Count != 0 && usersPass.Count != 0)
            {
                var session = new HousingSession(HttpContext.Session);
                session.SetName(UserEmail.ToUpper());
                return RedirectToAction("Dashboard", "Housing");

                
            }
            else
            {
                HttpContext.Session.Clear();
                return View();
            }
                
            
        }

        public IActionResult CreateNew()
        {
            return View();
        }
    }
}
