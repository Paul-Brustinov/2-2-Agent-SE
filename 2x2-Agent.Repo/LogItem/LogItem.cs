using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2x2_Agent.Repo.Schedule;

namespace _2x2_Agent.Repo
{
    public class LogItem : ILogItem
    {
        public enum LogKind {Start, Finish, Error};

        public int CodeResult { get; set; }
        public DateTime StartTime { get; set; }
        public int ScheduleId { get; set; }

        public LogKind Kind { get; set; }
        public string Description { get; set; }
        public string TaskName { get; set; }
    }
}
