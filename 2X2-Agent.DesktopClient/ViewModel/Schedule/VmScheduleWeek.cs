using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using _2x2_Agent.Repo;
using _2x2_Agent.Repo.Repo;
using _2x2_Agent.Repo.Schedule;
using _2x2_Agent.Repo.Task;
using _2X2_Agent.DesktopClient.View;
using _2X2_Agent.DesktopClient.View.Task;
using _2X2_Agent.DesktopClient.ViewModel.Common;
using _2X2_Agent.DesktopClient.ViewModel.Schedule.Common;
using VwSelectTask = _2X2_Agent.DesktopClient.View.Task.VwSelectTask;

namespace _2X2_Agent.DesktopClient.ViewModel.Schedule
{
    public class VmScheduleWeek : VmBaseSchedule
    {

        private static VmScheduleWeek _model;

        public new static VmScheduleWeek Model
        {
            get { return _model; }
        }

        public VmScheduleWeek()
        {
            _model = this;
        }

        public ScheduleWeek SelectedSchedule => (ScheduleWeek) VmMainWindow.Model.SelectedSchedule;

        public ScheduleTask SelectedTask => SelectedSchedule.ScheduleTask;
        public string AddUpdateText => SelectedSchedule.ScheduleTask == null ? "Add" : "Edit";

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

        #region Week

        private bool getDayOfWeekChecked(ScheduleWeek schedule, DayOfWeek day) => schedule.SchedDays.Contains(day);

        private void setDayOfWeekChecked(ScheduleWeek schedule, DayOfWeek day, bool value)
        {
            if (value && schedule.SchedDays.Contains(day)) return;
            if (!value && !schedule.SchedDays.Contains(day)) return;

            if (value)
            {
                schedule.SchedDays.Add(day);
            }
            else
            {
                schedule.SchedDays.Remove(day);
            }
        }

        public bool Monday
        {
            get { return getDayOfWeekChecked((ScheduleWeek)SelectedSchedule, DayOfWeek.Monday); }
            set { setDayOfWeekChecked((ScheduleWeek)SelectedSchedule, DayOfWeek.Monday, value); }
        }

        public bool Tuesday
        {
            get { return getDayOfWeekChecked((ScheduleWeek)SelectedSchedule, DayOfWeek.Tuesday); }
            set { setDayOfWeekChecked((ScheduleWeek)SelectedSchedule, DayOfWeek.Tuesday, value); }
        }
        public bool Wednesday
        {
            get { return getDayOfWeekChecked((ScheduleWeek)SelectedSchedule, DayOfWeek.Wednesday); }
            set { setDayOfWeekChecked((ScheduleWeek)SelectedSchedule, DayOfWeek.Wednesday, value); }
        }

        public bool Thursday
        {
            get { return getDayOfWeekChecked((ScheduleWeek)SelectedSchedule, DayOfWeek.Thursday); }
            set { setDayOfWeekChecked((ScheduleWeek)SelectedSchedule, DayOfWeek.Thursday, value); }
        }
        public bool Friday
        {
            get { return getDayOfWeekChecked((ScheduleWeek)SelectedSchedule, DayOfWeek.Friday); }
            set { setDayOfWeekChecked((ScheduleWeek)SelectedSchedule, DayOfWeek.Friday, value); }
        }
        public bool Saturday
        {
            get { return getDayOfWeekChecked((ScheduleWeek)SelectedSchedule, DayOfWeek.Saturday); }
            set { setDayOfWeekChecked((ScheduleWeek)SelectedSchedule, DayOfWeek.Saturday, value); }
        }
        public bool Sunday
        {
            get { return getDayOfWeekChecked((ScheduleWeek)SelectedSchedule, DayOfWeek.Sunday); }
            set { setDayOfWeekChecked((ScheduleWeek)SelectedSchedule, DayOfWeek.Sunday, value); }
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
            //Schedules.Add(SelectedSchedule);
            VmMainWindow.Model.OnPropertyChanged("SelectedSchedule");
            VmMainWindow.Model.OnPropertyChanged("Schedules");

            Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            win?.Close();
        }
        #endregion

    }
}
