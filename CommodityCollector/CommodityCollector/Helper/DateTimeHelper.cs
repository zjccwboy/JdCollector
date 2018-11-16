using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityCollector.Helper
{
    public class DateTimeHelper
    {
        public static DateTime UnixStampToDateTime(uint unixTime)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var TranslateDate = startTime.AddSeconds(unixTime);
            return TranslateDate;
        }

        public static uint DataTimeToUnixStamp(DateTime dateTime)
        {
            var unixStamp = (dateTime.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return (uint)unixStamp;
        }
    }
}
