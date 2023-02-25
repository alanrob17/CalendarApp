using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Data
{
    public static class DateTimeExtensions
    {
        public static string OrdinalSuffix(this int day)
        {
            if (day < 1 || day > 31)
            {
                throw new ArgumentException("Day must be between 1 and 31");
            }

            switch (day % 10)
            {
                case 1 when day != 11:
                    return "st";
                case 2 when day != 12:
                    return "nd";
                case 3 when day != 13:
                    return "rd";
                default:
                    return "th";
            }
        }
    }
}
