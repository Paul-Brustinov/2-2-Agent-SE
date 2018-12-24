using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2x2_Agent.Repo.Task
{
    public class ScheduleTask : ITask
    {
        public string ClassName => this.GetType().FullName;
        public string ShortClassName => this.GetType().Name;

        public override string ToString()
        {
            return ShortClassName;
        }

        public Task<List<LogItem>> StartAsync()
        {
            return new Task<List<LogItem>>(()=> new List<LogItem>()
            {
                new LogItem()
                {
                    Description = "ScheduleTask is not defined",
                    Kind = LogItem.LogKind.Start,
                    CodeResult = -1,
                    StartTime = DateTime.Now,
                    TaskName = this.ClassName,
                    ScheduleId = 0
                },
                new LogItem()
                {
                    Description = "ScheduleTask is not defined",
                    Kind = LogItem.LogKind.Finish,
                    CodeResult = -1,
                    StartTime = DateTime.Now,
                    TaskName = this.ClassName,
                    ScheduleId = 0
                }
            });
        }

        public int Id { get; set; }

        public string ShortDescription => "There isn't description!";
        public string Memo { get; set; }
    }
}
