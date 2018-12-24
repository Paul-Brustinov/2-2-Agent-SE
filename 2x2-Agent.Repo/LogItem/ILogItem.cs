using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2x2_Agent.Repo.Schedule;

namespace _2x2_Agent.Repo
{
    public interface ILogItem
    {
        DateTime StartTime { get; set; }
        int ScheduleId { get; set; }
        int CodeResult { get; set; }
        string Description { get; set; }
        string TaskName { get; set; }    
    }
}
