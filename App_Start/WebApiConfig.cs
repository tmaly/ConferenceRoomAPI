using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;
using WebApiContrib.Formatting.Jsonp; 

namespace ConferenceRoomAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Rooms",
                routeTemplate: "api/ConferenceRooms",
                defaults: new { controller = "Rooms" }
            );

            config.Routes.MapHttpRoute(
                name: "RoomInformation",
                routeTemplate: "api/ConferenceRooms/{id}/information",
                defaults: new { controller = "Rooms" }
            );

            config.Routes.MapHttpRoute(
                name: "ScheduleToday",
                routeTemplate: "api/ConferenceRoom/{id}/schedule/today",
                defaults: new { controller = "RoomSchedule" }
            );

            config.Routes.MapHttpRoute(
                name: "ScheduleOnDate",
                routeTemplate: "api/ConferenceRoom/{id}/schedule/{date}",
                defaults: new { controller = "RoomSchedule" }
            );

            config.Routes.MapHttpRoute(
                name: "ScheduleForDateRange",
                routeTemplate: "api/ConferenceRoom/{id}/schedule/{startdate}/{enddate}",
                defaults: new { controller = "RoomSchedule" }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            //jsonFormatter.SerializerSettings.Converters.Add(new ConferenceRoomAPI.Models.ToLocalTimeZoneSerializer());
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Add support JSONP
            var formatter = new JsonpMediaTypeFormatter(jsonFormatter, "cb");
            config.Formatters.Insert(0, formatter);

            // Add support CORS
            var attr = new EnableCorsAttribute("*", "*", "GET");
            config.EnableCors(attr);

        }
    }
}
