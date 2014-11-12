using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonModel;
using Office365Service;

namespace ConferenceRoomAPI.Controllers
{
    public class RoomsController : BaseController
    {
        /// <summary>
        ///  Gets a list of mailboxes for the configured Conf. Rooms.
        /// </summary>
        public HttpResponseMessage Get()
        {
            var list = this.srv.GetRooms();
            return Request.CreateResponse(HttpStatusCode.OK, new { ConferenceRooms = list }, "application/json");
        }

        /// <summary>
        ///  Gets information associated with a Conference Room.
        /// </summary>
        public HttpResponseMessage Get(string id)
        {
            var _id = this.getRoom(id);

            if (_id != null)
            {
                var attr = srv.GetRoomProperties(_id.MailBox);
                return Request.CreateResponse(HttpStatusCode.OK, new { RoomAttributes = attr }, "application/json");
            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, new { }, "application/json");
        }
    }
}
