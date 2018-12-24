using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using _2x2_Agent.Repo.Schedule;
using _2x2_Agent.Repo;
using _2x2_Agent.Repo.Task;

namespace _2x2_Agent.Repo
{
    public class Schedules : IRepo<ISchedule>
    {

        public ObservableCollection<ISchedule> List { get; set; }

        public Schedules()
        {
            List = new ObservableCollection<ISchedule>();
            Load();
        }

        public void Load()
        {
            //List.Add(
            //    new ScheduleWeekOneTime(new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday },
            //        new TimeSpan(15, 10, 0), 1)
            //    { ScheduleTask = new BackupAll() }
            //    );

            //using (StreamReader file = File.OpenText(@"schedules.json"))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    var l = (List<ScheduleWeekOneTime>)serializer.Deserialize(file, typeof(List<ScheduleWeekOneTime>));
            //    l.ForEach(s=>List.Add(s));

            //    //List = (IList<ISchedule>)l;
            //}

            List.Clear();


            if (!File.Exists(@"schedules.json"))
            {
                var f = File.Create(@"schedules.json");
                f.Close();
            }
            using (StreamReader file = File.OpenText(@"schedules.json"))
            {
                //string s = file.ReadToEnd();
                //List<ExpandoObject> obj = JsonConvert.DeserializeObject<List<ExpandoObject>>(s);

                JsonSerializer serializer = new JsonSerializer();
                List<ExpandoObject> obj = (List<ExpandoObject>)serializer.Deserialize(file, typeof(List<ExpandoObject>));
                if (obj == null) return;
                obj.ForEach((o =>
                {
                    string className = o.FirstOrDefault(p=>p.Key== "ClassName").Value.ToString();

                    //o.FirstOrDefault(p => p.Key == "ScheduleTask").Value.FirstOrDefault(p => p.Key == "ClassName")

                    ExpandoObject tmpTaskExpando = (ExpandoObject)o.FirstOrDefault(p => p.Key == "ScheduleTask").Value;
                    string taskName = tmpTaskExpando.FirstOrDefault(p => p.Key == "ClassName").Value.ToString();


                    //o.FirstOrDefault(p => p.Key == "TaskString").Value.ToString();
                    var schedule = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(className);

                    Type typeTask = Type.GetType(taskName);
                    var task = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(taskName);
                    if (task == null)
                    {
                        //throw new Exception($"Unknown class {taskName}!");

                    }
                    else { 
                        task = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(tmpTaskExpando), typeTask);
                    }

                    //var tmp = o.FirstOrDefault(p => p.Key == "Task").Value;
                    //task = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(tmp), typeTask);

                    Type typeSchedule = Type.GetType(className);
                    try
                    {
                        schedule = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(o), typeSchedule);
                        ((Schedule.Schedule) schedule).ScheduleTask = (ScheduleTask) task;
                        List.Add((ISchedule) schedule);
                    }
                    catch
                    {
                    }
                }));

            }




        }



        public void Save()
        {
            File.WriteAllText(@"schedules.json", JsonConvert.SerializeObject(List));
        }

        public void Add(ISchedule schedule)
        {
            int maxId = List.Count == 0?1:List.Max(x => x.Id) + 1;
            schedule.Id = maxId;
            List.Add(schedule);
        }

        public void AppendSave()
        {
            throw new NotImplementedException();
        }
    }
}
