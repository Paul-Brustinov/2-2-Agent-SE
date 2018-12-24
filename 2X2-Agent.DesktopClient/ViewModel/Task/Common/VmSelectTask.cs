using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using _2x2_Agent.Repo.Repo;
using _2X2_Agent.DesktopClient.View;
using _2X2_Agent.DesktopClient.ViewModel.Common;
using _2X2_Agent.DesktopClient.ViewModel.Schedule.Common;

namespace _2X2_Agent.DesktopClient.ViewModel.Task.Common
{
    class VmSelectTask : VmBase
    {
        public TaskTypes Tasks { get; set; } = new TaskTypes();
        
        public _2x2_Agent.Repo.Schedule.Schedule SelectedSchedule => (_2x2_Agent.Repo.Schedule.Schedule) VmMainWindow.Model.SelectedSchedule;

        public int SelectedIndex
        {
            get
            {
                if (SelectedSchedule == null) return 0;

                return Tasks.ToList().FindIndex(x => x.ClassName == SelectedSchedule.TaskName);
            }
            set
            {
                SelectedSchedule.TaskName = Tasks[value].ClassName;
            }
        }

        #region Exit
        private VmCommand _exit;
        public ICommand Exit => _exit ?? (_exit = new VmCommand(ExecuteExit));

        private void ExecuteExit(object a)
        {
            VmMainWindow.Model.OnPropertyChanged("SelectedSchedule");
            VmMainWindow.Model.OnPropertyChanged("Schedules");



            //var scheduleType = Type.GetType("_2X2_Agent.DesktopClient.ViewModel." +
                         //VmMainWindow.Model.SelectedSchedule.ShortClassName + "ViewModel");

            //var s =  (VmBaseSchedule)Activator.CreateInstance(scheduleType);

            //var m = (VmBaseSchedule)scheduleType.GetProperty("Model").GetValue(null);
            var m = VmClassLocator.GetScheduleViewModel(VmMainWindow.Model.SelectedSchedule);
            m.OnPropertyChanged("SelectedTask");
            m.OnPropertyChanged("AddUpdateText");


            //VmSheduleWeek.Model.OnPropertyChanged("SelectedTask");
            //VmSheduleWeek.Model.OnPropertyChanged("AddUpdateText");

            Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            win?.Close();
            if (SelectedSchedule.ScheduleTask == null) return;

            VmClassLocator.GetView(SelectedSchedule.ScheduleTask, VmClassLocator.NameSpaceTaskView,  "Edit")?.ShowDialog();

            //switch (SelectedSchedule.ScheduleTask.ShortClassName)
            //{
            //    case "BackupAll":
            //       (new VwEditBackupAll()).ShowDialog();
            //        break;
            //    default:
            //        break;
            //}
        }
        #endregion
    }
}
