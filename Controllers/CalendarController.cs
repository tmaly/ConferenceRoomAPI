using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonModel;
using Office365Service;
using ConferenceRoomAPI.Models;

namespace ConferenceRoomAPI.Controllers
{
    public class CalendarController : Controller
    {
        private Services srv;

        public CalendarController()
        {
            srv = new Services();
        }

        [HttpGet]
        public ActionResult DayView()
        {
            var list = srv.GetRooms();
            var model = new DayView();
            model.Events = new { };
            var date = DateTime.Now;
            model.Date = string.Format("{0}-{1}-{2}", date.Year, date.Month, date.Day);

            model.ConfRooms = new SelectList(list, "MailBox", "MailBox");

            return View(model);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var list = srv.GetRooms();
            var model = new SearchCriteria();
            model.Events = new { };
            var date = DateTime.Now;
            model.StartDate = string.Format("{0}-{1}-{2}", date.Year, date.Month, date.Day);

            model.ConfRooms = new SelectList(list, "MailBox", "MailBox");

            return View(model);
        }

        // POST: Calendar/Create
        [HttpPost]
        public ActionResult GetCalendar(SearchCriteria model)
        {
            try
            {
                if(model.StartDate.IsDate() && model.EndDate.IsDate())
                {
                    var data = srv.GetRoomScheduleForDateRange(model.SelectedRoom, model.StartDate.ToDate().Value, model.EndDate.ToDate().Value);

                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                    var events = new List<CalendarEvent>();
                    data.ForEach(d => 
                    {
                        var utcDate = d.EndDate.ToUniversalTime();
                        DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(utcDate, cstZone);
                        d.EndDate = cstTime;

                        utcDate = d.StartDate.ToUniversalTime();
                        cstTime = TimeZoneInfo.ConvertTimeFromUtc(utcDate, cstZone);
                        d.StartDate = cstTime;

                        events.Add(new CalendarEvent 
                        {
                            title = d.Subject,
                            start = d.StartDate.ToString(),
                            end = d.EndDate.ToString()
                        });
                    
                    });
                    model.Events = Json(events);

                    var list = srv.GetRooms();
                    model.ConfRooms = new SelectList(list, "MailBox", "MailBox");                
                
                }
                
                // TODO: Add insert logic here
                return View("Index", model);
            }
            catch
            {
                return View();
            }
        }
    }
}
