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
    public class AdminController : Controller
    {
        private Services srv;

        public AdminController()
        {
            srv = new Services();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
