using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2x2_Agent.Repo.Schedule
{
    public class ScheduleMonth : Schedule, ISchedule
    {
        public TimeSpan StartTime { get; set; }


        private int _day = 1;
        public int Day
        {
            get { return _day; }
            set
            {
                if (value < 1 || value > 31) throw new Exception("Day must be between 1 and 31");
                _day = value;
            }
        }

        public int MinToStart
        {
            get
            {
                int tmpDay = _day;
                DateTime now = DateTime.Now;

                if (tmpDay > DateTime.DaysInMonth(now.Year, now.Month))
                    tmpDay = DateTime.DaysInMonth(now.Year, now.Month);

                DateTime n = new DateTime(now.Year, now.Month, tmpDay, StartTime.Hours, StartTime.Minutes, 0);
                if (now.Day > tmpDay) n = n.AddMonths(1);
                return (n - now).Minutes;
            }
        }

        public int MinFromStart
        {
            get
            {
                int tmpDay = _day;
                DateTime now = DateTime.Now;

                if (tmpDay > DateTime.DaysInMonth(now.Year, now.Month))
                    tmpDay = DateTime.DaysInMonth(now.Year, now.Month);

                DateTime n = new DateTime(now.Year, now.Month, tmpDay, StartTime.Hours, StartTime.Minutes, 0);
                if (now.Day < tmpDay) n = n.AddMonths(-1);
                return (now - n).Minutes;
            }
        }
    }
}
