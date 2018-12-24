using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using _2x2_Agent.Repo.Schedule;

namespace _2x2_Agent.Repo
{
    public class Log : IRepo<ILogItem>
    {

        public Log()
        {
            Load();
        }

        public List<LogItem> List { get; set; }

        public void Load()
        {
            //List = new List<ILogItem>();
            using (StreamReader file = File.OpenText(@"log.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                List = (List<LogItem>)serializer.Deserialize(file, typeof(List<LogItem>));
            }
            if (List == null) List = new List<LogItem>();
        }

        public DateTime GetLastScheduleStart(int id)
        {
            if (List == null) return DateTime.MinValue;
            var l = List.Where(x => x.ScheduleId == id);
            return l.Any() ? l.Max(x => x.StartTime) : DateTime.MinValue;
        }


        public void Save()
        {
            foreach (var logItem in List)
            {
                Console.WriteLine("CodeResult: {0} ScheduleId: {1} CodeResult: {2} TaskName: {3} Description: {4}", logItem.StartTime, logItem.ScheduleId,  logItem.CodeResult, logItem.TaskName, logItem.Description);
            }

            File.WriteAllText(@"log.json", JsonConvert.SerializeObject(List));
        }

        public void AppendSave()
        {
            throw new NotImplementedException();
        }

        public ILogItem GetItemById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
