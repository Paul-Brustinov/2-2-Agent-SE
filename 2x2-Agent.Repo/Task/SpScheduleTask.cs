using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2x2_Agent.Repo.Task
{
    public class SpScheduleTask : ScheduleTask, ITask
    {
        public string SpName { get; set; }

        public string ConnectionString { get; set; }

        public new Task<List<LogItem>> StartAsync()
        {

            var res = new List<LogItem>(){
                new LogItem()
                {
                    CodeResult = 0,
                    ScheduleId = 0,
                    StartTime = DateTime.Now,
                    Kind = LogItem.LogKind.Start,
                    Description = "",
                    TaskName = ClassName
                }};


            try
            {
                SpExecuter.Run(SpName, ConnectionString);
                res.Add(new LogItem()
                {
                    CodeResult = 0,
                    ScheduleId = 0,
                    StartTime = DateTime.Now,
                    Kind = LogItem.LogKind.Finish,
                    Description = "",
                    TaskName = ClassName
                });
            }
            catch (Exception e)
            {
                res.Add(new LogItem()
                {
                    CodeResult = -1,
                    ScheduleId = 0,
                    StartTime = DateTime.Now,
                    Kind = LogItem.LogKind.Error,
                    Description = e.Message,
                    TaskName = ClassName
                });
            }

            return new Task<List<LogItem>>(() => res);
        }
    }
}
