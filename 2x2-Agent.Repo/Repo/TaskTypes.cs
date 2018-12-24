using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using _2x2_Agent.Repo.Task;


namespace _2x2_Agent.Repo.Repo
{
    public class TaskTypes : List<ITask>
    {
        public TaskTypes()
        {
            //Add(new BackupAll());
            //Add(new SpScheduleTask());

            var tasksTypes = Assembly.GetAssembly(typeof(ScheduleTask)).GetTypes().Where(t => t.IsSubclassOf(typeof(ScheduleTask)));
            foreach (var task in tasksTypes)
            {
                Add((ITask) Activator.CreateInstance(task));
            }

        }
    }
}
