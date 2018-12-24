using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using _2x2_Agent.Repo.Task;
using _2X2_Agent.DesktopClient.ViewModel.Common;
using _2X2_Agent.DesktopClient.ViewModel.Schedule;
using _2X2_Agent.DesktopClient.ViewModel.Schedule.Common;

namespace _2X2_Agent.DesktopClient.ViewModel.Task
{
    class VmBackupAll
    {
        public ObservableCollection<string> ConnStringsAliases { get; } = new ObservableCollection<string>();

        public ObservableCollection<BackupKind> BackupKinds { get; } = new ObservableCollection<BackupKind>() {BackupKind.Full, BackupKind.Different};

        public int SelectedBackupKind
        {
            get { return (int)SelectedTask.BackupKind; }
            set { SelectedTask.BackupKind = (BackupKind) value; }
        }

        public VmBackupAll()
        {
            var connStrings = System.Configuration.ConfigurationManager.ConnectionStrings;
            
            foreach (ConnectionStringSettings connString in connStrings)
            {
                if (connString.Name != "OraAspNetConString" && connString.Name != "LocalSqlServer") ConnStringsAliases.Add(connString.Name);
            }
        }

        public int SelectedAlias
        {
            get
            {
                return ConnStringsAliases.IndexOf(SelectedTask.ConnectionAlias);
            }
            set
            {
                SelectedTask.ConnectionAlias = ConnStringsAliases[value];
            }
        }

        //public ISchedule SelectedSchedule => SchedulesViewModel.Model.SelectedSchedule;
        public BackupAll SelectedTask => (BackupAll)VmMainWindow.Model.SelectedSchedule.ScheduleTask;


        #region Exit
        private VmCommand _exit;
        public ICommand Exit => _exit ?? (_exit = new VmCommand(ExecuteExit));

        private void ExecuteExit(object a)
        {
            VmMainWindow.Model.OnPropertyChanged("SelectedSchedule");
            VmMainWindow.Model.OnPropertyChanged("Schedules");


            //var scheduleType = Type.GetType("_2X2_Agent.DesktopClient.ViewModel." +
            //             VmMainWindow.Model.SelectedSchedule.ShortClassName + "ViewModel");

            //var s =  (VmBaseSchedule)Activator.CreateInstance(scheduleType);
            //(VmBaseSchedule)scheduleType.GetProperty("Model").GetValue(null);
            var m = VmClassLocator.GetScheduleViewModel(VmMainWindow.Model.SelectedSchedule);
            m.OnPropertyChanged("SelectedTask");
            m.OnPropertyChanged("AddUpdateText");


            //VmSheduleWeek.Model.OnPropertyChanged("SelectedTask");
            //VmSheduleWeek.Model.OnPropertyChanged("AddUpdateText");

            Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            win?.Close();
        }
        #endregion

    }
}
