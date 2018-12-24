using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using _2x2_Agent.Repo.Schedule;
using _2x2_Agent.Repo.Task;
using _2X2_Agent.DesktopClient.View;
using _2X2_Agent.DesktopClient.View.Task;
using _2X2_Agent.DesktopClient.ViewModel.Common;
using _2X2_Agent.DesktopClient.ViewModel.Schedule.Common;
using VwSelectTask = _2X2_Agent.DesktopClient.View.Task.VwSelectTask;

namespace _2X2_Agent.DesktopClient.ViewModel.Schedule
{
    class VmScheduleMonth : VmBaseSchedule
    {
        private static VmScheduleMonth _model;
        public new static VmScheduleMonth Model
        {
            get { return _model; }
        }
        public VmScheduleMonth()
        {
            _model = this;
        }

        public ScheduleMonth SelectedSchedule => (ScheduleMonth)VmMainWindow.Model.SelectedSchedule;
        public ScheduleTask SelectedTask => SelectedSchedule.ScheduleTask;


        #region Time

        public int Hours
        {
            get { return SelectedSchedule.StartTime.Hours; }
            set
            {
                if (value < 0 || value > 24)
                    SelectedSchedule.StartTime = new TimeSpan(0, SelectedSchedule.StartTime.Minutes, 0);
                else
                {
                    SelectedSchedule.StartTime = new TimeSpan(value, SelectedSchedule.StartTime.Minutes, 0);
                }
            }
        }


        public int Minutes
        {
            get { return SelectedSchedule.StartTime.Minutes; }
            set
            {
                if (value < 0 || value > 59)
                    SelectedSchedule.StartTime = new TimeSpan(SelectedSchedule.StartTime.Hours, 0, 0);
                else
                {
                    SelectedSchedule.StartTime = new TimeSpan(SelectedSchedule.StartTime.Hours, value, 0);
                }
            }
        }

        #endregion

        #region AddUpdate

        private VmCommand _addUpdateVmCommand;
        public ICommand AddUpdateCommand => _addUpdateVmCommand ?? (_addUpdateVmCommand = new VmCommand(ExecuteAddUpdate));

        private void ExecuteAddUpdate(object a)
        {


            if (SelectedSchedule.ScheduleTask == null)
            {
                // call Add Window
                (new VwSelectTask()).ShowDialog();
            }
            else
            {
                VmClassLocator.GetView(SelectedSchedule.ScheduleTask, VmClassLocator.NameSpaceTaskView, "Edit")?.ShowDialog();
            }

        }
        #endregion

        #region DeleteTask
        private VmCommand _deleteTask;
        public ICommand DeleteTask => _deleteTask ?? (_deleteTask = new VmCommand(ExecuteDeleteTask));

        private void ExecuteDeleteTask(object a)
        {
            SelectedSchedule.ScheduleTask = null;
            this.OnPropertyChanged("SelectedTask");
        }
        #endregion

        #region Exit
        private VmCommand _exit;
        public ICommand Exit => _exit ?? (_exit = new VmCommand(ExecuteExit));

        private void ExecuteExit(object a)
        {
            if (!VmMainWindow.Schedules.Contains(SelectedSchedule)) VmMainWindow.SchedulesRepo.Add(SelectedSchedule);
            VmMainWindow.Model.OnPropertyChanged("SelectedSchedule");
            VmMainWindow.Model.OnPropertyChanged("Schedules");

            Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            win?.Close();
        }
        #endregion

    }
}
