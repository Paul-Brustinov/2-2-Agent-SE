using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using _2x2_Agent.Repo;
using _2x2_Agent.Repo.Task;

namespace _2x2_Agent.Repo.Schedule
{
    public class ScheduleWeek : Schedule, ISchedule
    {
        public IList<DayOfWeek> SchedDays { get; set; } = new List<DayOfWeek>();

        public TimeSpan StartTime { get; set; }

        public ScheduleWeek() : base(){}

        public ScheduleWeek(IList<DayOfWeek> _schedDays, TimeSpan _startTime, int _id) : this()
        {
            Id = _id;
            SchedDays = _schedDays;
            StartTime = _startTime;
        }

        [JsonIgnore]
        public int MinToStart { 
        get{
                DateTime now = DateTime.Now;
                
                int minDays = 7;
                foreach (var schedDay in SchedDays)
                {
                    int days = ScheduleCommon.GetDays(now.DayOfWeek, schedDay);
                    if (days == 0 && (now.Hour * 60 + now.Minute) - (StartTime.Hours * 60 + StartTime.Minutes) > 0) continue;
                    if (minDays > days) minDays = days;
                }
                int minutes = minDays * 24 * 60 - (now.Hour * 60 + now.Minute) + (StartTime.Hours * 60 + StartTime.Minutes);
                return minutes;
            }
        }

        [JsonIgnore]
        public int MinFromStart { 
            get{
                DateTime now = DateTime.Now;
                
                int minDays = 0;
                foreach (var schedDay in SchedDays)
                {
                    int days = 7 - ScheduleCommon.GetDays(now.DayOfWeek, schedDay);
                    if (minDays < days) minDays = days;
                }
                int minutes = minDays * 24 * 60 - (now.Hour * 60 + now.Minute) + (StartTime.Hours * 60 + StartTime.Minutes);
                return minutes;
            }
        }
    }
}
