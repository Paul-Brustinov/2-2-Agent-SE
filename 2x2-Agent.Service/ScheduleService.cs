using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using _2x2_Agent.Repo;
using _2x2_Agent.Repo.Schedule;

namespace _2x2_Agent.Service
{
    public partial class ScheduleService : ServiceBase
    {
        public ScheduleService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Schedules schedules = new Schedules();
            //schedules.Load(); // reading schedules from disc

            Log log = new Log();
            //log.Load(); // reading log from disc

            while (true) { 

                DateTime currentDateTime = DateTime.Now;    // start time

                int minMinutesToStart = 0;  // Time to execution nearest task

                List <ISchedule> StartList = new List<ISchedule>(); // Contains tasks to start

                // Check if there are a lated tasks
                foreach (var schedule in schedules.List)
                {
                    var lastStartDateTime = log.GetLastScheduleStart(schedule.Id);
                    var minutesFromStart = schedule.MinFromStart;

                    if (minutesFromStart == 0) continue;
                    if (minutesFromStart > ((currentDateTime - lastStartDateTime).TotalMinutes + 60 * 3))
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
                    StartList.OrderBy(s => s.MinToStart);
                    foreach (ISchedule schedule in StartList)
                    {
                        List<LogItem> res = schedule.ScheduleTask.StartAsync().Result;
                        log.List = log.List.Concat(res).ToList();
                        log.Save();
                    }
                }
                else // if there are not tasks to execute lets sleep
                {
                    if (minMinutesToStart == 0) return;
                    Thread.Sleep(minMinutesToStart * 60 * 1000);
                }
            }
        }

        protected override void OnStop()
        {
        }
    }
}
