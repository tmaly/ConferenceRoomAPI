using CommonModel;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRoomAPI.Models
{
    public class ToLocalTimeZoneSerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            // "Central Standard Time"   
            TimeZoneInfo localZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

            var rs = value as RoomSchedule;
            writer.WriteStartObject();

            foreach (PropertyInfo p in rs.GetType().GetProperties())
            {
                object pval = p.GetValue(rs);

                if (p.PropertyType == typeof(DateTime))
                {
                    if (pval != null)
                    {
                        DateTime dval = (DateTime)pval;
                        var utcDate = dval.ToUniversalTime();
                        DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(utcDate, localZone);
                        pval = cstTime;
                    }
                    else
                        pval = string.Empty;
                }

                writer.WritePropertyName(p.Name);
                serializer.Serialize(writer, pval);
            }

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return this.ReadJson(reader, objectType, existingValue,serializer);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(LocalSchedule).IsAssignableFrom(objectType);
        }

        /*
        Takes the place of - 
        TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
        list.ForEach(a => 
        {
            var utcDate = a.EndDate.ToUniversalTime();
            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(utcDate, cstZone);
            a.EndDate = cstTime;

            utcDate = a.StartDate.ToUniversalTime();
            cstTime = TimeZoneInfo.ConvertTimeFromUtc(utcDate, cstZone);
            a.StartDate = cstTime;
        });
        */

    }
}