using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConferenceRoomAPI.Models
{
    public static class Date
    {
        public static bool IsDate(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            try
            {
                DateTime moo;
                if (DateTime.TryParse(str, out moo))
                    return true;
            }
            catch { }

            return false;
        }

        public static DateTime? ToDate(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            try
            {
                DateTime moo;
                if (DateTime.TryParse(str, out moo))
                    return moo;
            }
            catch { }

            return null;
        }

        public static List<string> GetDateWindow(this string str)
        {
            var date = str.ToDate();
            if (date != null)
            {
                return new List<string> { date.Value.AddDays(-1).ToString("yyyy-MM-dd"), date.Value.AddDays(1).ToString("yyyy-MM-dd") };
            }
            return new List<string>();
        }
    }
}