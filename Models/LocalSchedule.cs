using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using CommonModel;

namespace ConferenceRoomAPI.Models
{
    [JsonConverter(typeof(ToLocalTimeZoneSerializer))]
    public class LocalSchedule : RoomSchedule 
    {
        public LocalSchedule()
        {

        }

        public LocalSchedule(RoomSchedule model)
        {
            this.Duration = model.Duration;
            this.EndDate = model.EndDate;
            this.EventType = model.EventType;
            this.Organizer = model.Organizer;
            this.StartDate = model.StartDate;
            this.Subject = model.Subject; 
        }
    }
}