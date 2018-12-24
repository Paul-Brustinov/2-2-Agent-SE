using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using _2x2_Agent.Repo.Schedule;
using _2x2_Agent.Repo.Task;

namespace _2x2_Agent.Repo.Repo
{
    public class ScheduleTypes : List<ISchedule>
    {
        public ScheduleTypes()
        {
            //Add(new ScheduleWeekSameTime());
            //Add(new ScheduleMonth());
            var scheduleTypes = Assembly.GetAssembly(typeof(Schedule.Schedule)).GetTypes().Where(t => t.IsSubclassOf(typeof(Schedule.Schedule)));
            foreach (var schedule in scheduleTypes)
            {
                Add((ISchedule)Activator.CreateInstance(schedule));
            }
        }
    }
}
