using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2x2_Agent.Repo;
using _2x2_Agent.Repo.Task;

namespace _2x2_Agent.Repo.Schedule
{
    public interface ISchedule
    {
        int Id { get; set; }

        string ClassName { get; }

        string ShortClassName { get; }

        string TaskName { get; set; }
        string ShortTaskName { get; }
        string Memo { get; set; }
        DateTime StartDate { get; set; }

        int MinToStart { get; }
        int MinFromStart { get; }
        ScheduleTask ScheduleTask { get; set; }
        

    }
}
