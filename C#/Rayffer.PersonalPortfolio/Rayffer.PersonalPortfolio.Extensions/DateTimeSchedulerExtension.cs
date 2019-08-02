using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.Extensions
{
    public static class DateTimeSchedulerExtension
    {
        public static List<DateTime> CreateWeekSchedule(this DateTime startDate, DateTime endDate, uint weekInterval, List<DayOfWeek> scheduleWeekDays)
        {
            var scheduleDates = new List<DateTime>();

            for (var dt = startDate; dt < endDate; dt = dt.AddDays(1))
            {
                if (scheduleWeekDays.Any(scheduleWeekDay => scheduleWeekDays.Equals(dt.DayOfWeek)))
                {
                    var lastCurrentWeekDayScheduledDate = scheduleDates.LastOrDefault(scheduleDate => scheduleDate.DayOfWeek.Equals(dt.DayOfWeek));

                    // To control the case in which there are no scheduled dates for the current schedule day we compare the lastCurrentWeekDayScheduledDate 
                    // field to its default value.
                    // We also control if the week interval is 0 in order to replicate the behaviour of a weekInterval of 1 week.
                    // Lastly, we compare the current date to the lastCurrentWeekDayScheduledDate field, retrieve the days from that timespan and
                    // do a modulo operation of 7 multiplied by the week interval to find if the day is suitable to be added to the list of scheduled days.
                    if (lastCurrentWeekDayScheduledDate.Equals(DateTime.MinValue)
                        || weekInterval.Equals(0)
                        || ((dt - lastCurrentWeekDayScheduledDate).Days % (7 * weekInterval)).Equals(0))
                    {
                        scheduleDates.Add(dt);
                    }
                }
            }

            return scheduleDates;
        }
    }
}
