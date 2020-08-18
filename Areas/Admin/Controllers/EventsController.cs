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

namespace CampChetek.Areas.Admin.Controllers
{
    [Route("[area]/[controller]/[action]")]
    [Area("Admin")]
    public class EventsController : Controller
    {
        private Persons context { get; set; }

        public EventsController(Persons ctx)
        {
            context = ctx;
        }
        //private DA _DA { get; set; }

        //public EventsController(IOptions<AppSettings> settings)
        //{
        //    _DA = new DataAccessLayer.DA(settings.Value.ConnectionStr);
        //}

        public IActionResult Calendar()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult GetCalendarEvents()
        //{
        //    var events = context.Houses.Select(e => new
        //    {
        //        eventId = e.ID,
        //        title = e.Title,
        //        description = e.RoomsId,
        //        StartTime = e.Start.ToString("MM/dd/yyyy h: mm A"),
        //        EndTime = e.End.ToString("MM/dd/yyyy h: mm A")
        //    });
        //    return new JsonResult(events);
        //}

        [HttpGet]
        public IActionResult GetCalendarEvents(string start, string end)
        {
            //start = DateTime.Now.ToString();
            //end = DateTime.Now.ToString();
            //List<GetCalendarEvents> events = _DA.GetCalendarEvents(start, end);


            //return Json(events);
            var eventsList = context.events.Select(e => new
            {
                id = e.event_id,
                title = e.title,
                description = e.description,
                start = e.event_start,
                end = e.event_end,
                allDay = e.all_day

            });


            return Json(eventsList);
        }

        [HttpPost]
        public IActionResult UpdateEvent([FromBody] GetCalendarEvents evt)
        {
            string message = String.Empty;

            //message = _DA.UpdateEvent(evt);

            return Json(new { message });
        }

        [HttpPost]
        public IActionResult AddEvent([FromBody] GetCalendarEvents evt)
        {
            string message = String.Empty;
            int eventId = 0;

            //message = _DA.AddEvent(evt, out eventId);

            return Json(new { message, eventId });
        }

        [HttpPost]
        public IActionResult DeleteEvent([FromBody] GetCalendarEvents evt)
        {
            string message = String.Empty;

            //message = _DA.DeleteEvent(evt.EventId);

            return Json(new { message });
        }
    }
}