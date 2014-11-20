using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CommonModel;

namespace ConferenceRoomAPI.Models
{
    public class DayView
    {
        public SelectList ConfRooms { get; set; }
        public string SelectedRoom { get; set; }
        public string RoomName { get; set; }

        public string Date { get; set; }
        public string MonthStartDate { get; set; }

        public dynamic Events { get; set; }
    }
}