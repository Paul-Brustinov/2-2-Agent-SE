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
    public abstract class Schedule
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public string ClassName => GetType().FullName;
        
        [JsonIgnore]
        public string ShortClassName => GetType().Name;

        //public Guid IdGuid { get; }

        public string Memo { get; set; }

        public ScheduleTask ScheduleTask { get; set; }

        public string TaskName
        {
            get { return ScheduleTask==null? "": ScheduleTask.ClassName; }
            set { ScheduleTask = (Task.ScheduleTask)System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(value); }
        }

        [JsonIgnore]
        public string ShortTaskName => ScheduleTask == null ? "" : ScheduleTask.ShortClassName;

        protected Schedule()
        {
            StartDate = DateTime.Now;
            //IdGuid = new Guid();
        }
    }
}
