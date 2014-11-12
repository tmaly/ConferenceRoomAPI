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
    public class BaseController : ApiController
    {
        protected List<RoomAttributes> _rooms;
        protected Services srv = new Services();

        public BaseController()
        {
             _rooms = srv.GetRooms(); 
        }

        protected RoomAttributes getRoom(string id)
        {
            if (string.IsNullOrEmpty(id) || _rooms == null || !_rooms.Any())
                return null;

            return _rooms.FirstOrDefault(s => s.MailBox.ToUpper() == id.ToUpper());
        }
    }
}