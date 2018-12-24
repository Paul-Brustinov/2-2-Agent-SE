using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2x2_Agent.Controller;
using _2x2_Agent.Repo;
using _2x2_Agent.Repo.Schedule;

namespace _2x2_Agent.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //SchedulerController controller = new SchedulerController();
            
            //controller.Start();


            var s = new Schedules();
            s.Load();
            ;

        }
    

        public static int GetDays(DayOfWeek start, DayOfWeek end)
        {

            if (start.CompareTo(end) <= 0)
                return ((int)end - (int)start);

            return 7 - (int)start + (int)end;

        }
    }
}
