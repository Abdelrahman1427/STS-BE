using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.SharedKernel.Helpers.Utilties
{
    public static class TimeZoneUtility
    {
        //This is used to transform time zone Id (IANA id) to a valid time span 
        // for example : "Africa/Cairo" will be (2,0,0) an so on
        public static TimeSpan GetTimeZoneAsOffset(string timeZoneId)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            var timeZoneAsOffset = timeZone.GetUtcOffset(DateTimeOffset.UtcNow);

            return timeZoneAsOffset;
        }
    }
}
