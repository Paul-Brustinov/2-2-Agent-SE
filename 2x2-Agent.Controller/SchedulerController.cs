using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using _2x2_Agent.Repo;
using _2x2_Agent.Repo.Schedule;

namespace _2x2_Agent.Controller
{
    public class SchedulerController
    {
        Schedules schedules = new Schedules();
        Log log = new Log();

        public SchedulerController()
        {
            //schedules.Load(); // reading schedules from disc
            //log.Load(); // reading log from disc            
            schedules.Save(); // reading schedules from disc            
        }

        public void Start()
        {
            while (true)
            {

                DateTime currentDateTime = DateTime.Now;    // start time

                int minMinutesToStart = 0;  // Time to execution nearest task

                List<ISchedule> StartList = new List<ISchedule>(); // Contains tasks to start

                // Check if there are a lated tasks
                foreach (var schedule in schedules.List)
                {
                    var lastStartDateTime = log.GetLastScheduleStart(schedule.Id);
                    var minutesFromStart = schedule.MinFromStart;

                    if (minutesFromStart == 0) continue;
                    if (minutesFromStart + 60 * 3 < ((currentDateTime - lastStartDateTime).TotalMinutes))
                    {
                        StartList.Add(schedule);
                    }
                }

                // Adding next tasks
                foreach (var schedule in schedules.List)
                {
                    var minutesToStart = schedule.MinToStart;
                    if (minMinutesToStart < minutesToStart) minMinutesToStart = minutesToStart;
                    if (minutesToStart >= 1) continue;
                    if (!StartList.Contains(schedule)) StartList.Add(schedule);
                }

                // execution of added tasks
                if (StartList.Count > 0)
                {
                    StartList = StartList.OrderBy(s => s.MinToStart).ToList();
                    foreach (ISchedule schedule in StartList)
                    {
                        List<LogItem> res = schedule.ScheduleTask.StartAsync().Result;
                        log.List = log.List.Concat(res).ToList();
                        foreach (var logItem in log.List)
                        {
                            logItem.ScheduleId = schedule.Id;
                        }
                        log.Save();
                    }
                }
                else // if there are not tasks to execute lets sleep
                {
                    if (minMinutesToStart == 0) return;
                    Console.WriteLine("next start in {0} minutes", minMinutesToStart);
                    Thread.Sleep(minMinutesToStart * 60 * 1000);
                }
            }
        }



    }
}
