using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2x2Soft.Infrastructure;
using _2x2_Agent.Repo;

namespace _2x2_Agent.Repo.Task
{

    public enum BackupKind
    {
        Full,
        Different
    }

    public class BackupAll : ScheduleTask, ITask
    {

        public BackupAll()
        {
            ConnectionAlias = "Default";
        }

        public string Folder { get; set; } = @"d:\Backup";

        public bool CompressBackup { get; set; } = true;

        public BackupKind BackupKind { get; set; } = BackupKind.Full;

        public string Postfix { get; set; } = "init";

        public string Prefix { get; set; } = "";

        public bool SetDataTimeStamp { get; set; } = true;

        public new string ShortDescription => $"{ShortClassName} Folder:{Folder}, ConnectionAlias:{ConnectionAlias}";
        
        public string ConnectionAlias { get; set; }

        protected IDbContext _dbContext;

        public new async Task<List<LogItem>> StartAsync()
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
                //SpExecuter.RunAlias("MakeFullBackup", ConnectionAlias);

                await _dbContext.ExecuteAsync(null, "dbo._BackUpAllDb2", new {Folder = Folder, CompressBackup = CompressBackup?1:0, BackupKind = (int)BackupKind, Postfix = Postfix, Prefix = Prefix, AddTimeStamp = SetDataTimeStamp?1:0});

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
            return res;
        }
    }
}
