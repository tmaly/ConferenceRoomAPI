using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonModel;
using ConferenceRoomAPI.Models;
using Office365Service;

namespace ConferenceRoomAPI.Controllers
{
    public class RoomScheduleController : BaseController
    {
        /// <summary>
        ///  Gets the current schedule for a Conference Room.
        /// </summary>
        public HttpResponseMessage Get(string id)
        {
            try 
            {
                // api/ConferenceRoom/{id}/schedule/today

                var _id = this.getRoom(id);
                if (_id != null)
                {
                    var list = srv.GetRoomsCurrentSchedule(_id.MailBox);
                    return Request.CreateResponse(HttpStatusCode.OK, list, "application/json");
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, new List<Room>(), "application/json");

            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        ///  Gets the schedule for a Conference Room on the specified date.
        /// </summary>
        public HttpResponseMessage Get(string id, string date)
        {
            try
            {
                // api/ConferenceRoom/{id}/schedule/{date}
                var _id = this.getRoom(id);

                if (_id != null && date.IsDate())
                {                    
                    var list = _toLocal(srv.GetRoomScheduleForDate(_id.MailBox, date.ToDate().Value));                    
                    return Request.CreateResponse(HttpStatusCode.OK, list, "application/json");
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CR Mailbox and Date Required");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <param name="id">
        ///  Mailbox of Conf. Room: xxx@yyy.zzz
        /// </param> 
        /// <param name="startdate">
        ///  Date Format should be mm-dd-yyyy
        /// </param> 
        /// <param name="enddate">
        ///  Date Format should be mm-dd-yyyy
        /// </param> 
        /// <summary>
        ///  Gets the schedule for a Conference Room during the specified date range.
        /// </summary>
        public HttpResponseMessage Get(string id, string startdate, string enddate)
        {
            try
            {
                var _id = this.getRoom(id);

                if (_id != null && startdate.IsDate() && enddate.IsDate())
                {
                    // api/ConferenceRoom/{id}/schedule/{date}
                    var srv = new Services();
                    var list = _toLocal(srv.GetRoomScheduleForDateRange(_id.MailBox, startdate.ToDate().Value, enddate.ToDate().Value));
                    return Request.CreateResponse(HttpStatusCode.OK, list, "application/json");
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "CR Mailbox and Date Required");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        private List<LocalSchedule> _toLocal(List<RoomSchedule> list)
        {
            var localList = new List<LocalSchedule>();
            list.ForEach(x =>
            {
                localList.Add(new LocalSchedule(x));
            });
            return localList;
        }
    }
}