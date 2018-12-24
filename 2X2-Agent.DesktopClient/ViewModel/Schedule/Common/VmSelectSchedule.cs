using System.Linq;
using System.Windows;
using System.Windows.Input;
using _2x2_Agent.Repo.Repo;
using _2x2_Agent.Repo.Schedule;
using _2X2_Agent.DesktopClient.View;
using _2X2_Agent.DesktopClient.View.Schedule;
using _2X2_Agent.DesktopClient.ViewModel.Common;

namespace _2X2_Agent.DesktopClient.ViewModel.Schedule.Common
{
    class VmSelectSchedule : VmBase
    {
        public ISchedule SelectedSchedule
        {
            get { return VmMainWindow.Model.SelectedSchedule; }
            set { VmMainWindow.Model.SelectedSchedule = value; }
        }

        public ScheduleTypes ScheduleTypes { get; } = new ScheduleTypes();

        public int SelectedIndex
        {
            get
            {
                SelectedSchedule = SelectedSchedule ?? (ScheduleTypes[0]);
                return ScheduleTypes.FindIndex(x => x.ClassName == SelectedSchedule.ClassName);
            }
            set { SelectedSchedule = ScheduleTypes[value]; }
        }

        #region Exit
        private VmCommand _exit;
        public ICommand Exit => _exit ?? (_exit = new VmCommand(ExecuteExit));

        private void ExecuteExit(object a)
        {
            VmMainWindow.Model.OnPropertyChanged("SelectedSchedule");
            VmMainWindow.Model.OnPropertyChanged("Schedules");

            Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            win?.Close();
            if (SelectedSchedule == null) return;

            VmClassLocator.GetView(SelectedSchedule, VmClassLocator.NameSpaceScheduleView, "Edit")?.ShowDialog();
        }
        #endregion

    }
}
