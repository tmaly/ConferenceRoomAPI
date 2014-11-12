using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CommonModel;

namespace ConferenceRoomAPI.Models
{
    public class SearchCriteria
    {
        public SelectList ConfRooms { get; set; }
        public string SelectedRoom { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public dynamic Events { get; set; }
    }
}