using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2x2_Agent.Repo.Schedule
{
    public static class ScheduleCommon
    {
        public static int GetDays(DayOfWeek start, DayOfWeek end)
        {

            if (start.CompareTo(end) <= 0)
                return ((int)end - (int)start);

            return 7 - (int)start + (int)end;

        }
    }
}
