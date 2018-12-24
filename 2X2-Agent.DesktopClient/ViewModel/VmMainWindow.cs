using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using _2x2_Agent.Repo;
using _2x2_Agent.Repo.Schedule;
using _2X2_Agent.DesktopClient.View;
using _2X2_Agent.DesktopClient.View.Schedule;
using _2X2_Agent.DesktopClient.ViewModel.Common;
using _2X2_Agent.DesktopClient.ViewModel.Schedule;
using SelectScheduleType = _2X2_Agent.DesktopClient.View.Schedule.SelectScheduleType;

namespace _2X2_Agent.DesktopClient.ViewModel
{
    public class VmMainWindow : VmBase
    {

        private static VmMainWindow _model;
        public static VmMainWindow Model=>_model;

        public VmMainWindow()
        {
            _model = this;
        }

        public static Schedules SchedulesRepo = new Schedules();
        public static ObservableCollection<ISchedule> Schedules { get; set; } = SchedulesRepo.List;

        public static VwEditScheduleWeek VwEditScheduleWeek { get; set; }

        private static ISchedule _selectedSchedule; 
        public ISchedule SelectedSchedule {
            get
            {
                //TODO: how to avoid this code?
                ICollectionView view = CollectionViewSource.GetDefaultView(Schedules);
                view.Refresh();
                return _selectedSchedule;
            }
            set
            {
                _selectedSchedule = value;
                this.OnPropertyChanged("SelectedSchedule");
            }
        }

        #region Delete
        private VmCommand _delete;
        public ICommand Delete => _delete ?? (_delete = new VmCommand(ExecuteDelete));

        private void ExecuteDelete(object a)
        {
            if (SelectedSchedule == null) { MessageBox.Show("Schedule does not selected!"); return;}

            if(MessageBox.Show("Do you want to delete this schedule?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No) == MessageBoxResult.Yes)
                Schedules.Remove(_selectedSchedule);
        }
        #endregion

        #region Add
        //private VmCommand _add;
        //public ICommand Add => _add ?? (_add = new VmCommand(ExecuteAdd));

        //private static void ExecuteAdd(object a)
        //{
        //    _selectedSchedule = new ScheduleWeek();
        //    Schedules.Add(_selectedSchedule);
        //    VwEditScheduleWeek = new VwEditScheduleWeek();
        //    VwEditScheduleWeek.Show();
        //}
        #endregion

        #region Add2

        private VmCommand _add2;
        public ICommand Add2 => _add2 ?? (_add2 = new VmCommand(ExecuteAdd2));

        private void ExecuteAdd2(object a)
        {
            (new SelectScheduleType()).ShowDialog();
        }
        #endregion

        #region Save
        private VmCommand _save;
        public ICommand Save => _save ?? (_save = new VmCommand(ExecuteSave));

        private void ExecuteSave(object a)
        {
            SchedulesRepo.Save();
        }
        #endregion


        #region Exit
        private VmCommand _exit;
        public ICommand Exit => _exit ?? (_exit = new VmCommand(ExecuteExit));

        private void ExecuteExit(object a)
        {
            Application.Current.Shutdown();
        }
        #endregion



        #region EditScheduleCommand
        private VmCommand _editScheduleVmCommand;

        public ICommand EditScheduleCommand => _editScheduleVmCommand ??
                                                    (_editScheduleVmCommand = new VmCommand(ExecuteEditSchedule));

        //static EditScheduleWeekSameTimeCommand VwEditScheduleWeek;
        private void ExecuteEditSchedule(object a)
        {
            //switch (SelectedSchedule.ShortClassName)
            //{ 
            //    case "ScheduleWeekSameTime":
            //        (new VwEditScheduleWeek()).ShowDialog();
            //        break;
            //    case "ScheduleMonth":
            //        (new VwEditScheduleMonth()).ShowDialog();
            //        break;
            //}
            VmClassLocator.GetView(SelectedSchedule, VmClassLocator.NameSpaceScheduleView, "Edit")?.ShowDialog();
            OnPropertyChanged("SelectedSchedule");
            OnPropertyChanged("Schedules");
        }
        #endregion

    }
}
