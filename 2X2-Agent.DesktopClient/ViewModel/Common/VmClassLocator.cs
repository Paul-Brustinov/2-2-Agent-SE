using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _2x2_Agent.Repo.Schedule;
using _2X2_Agent.DesktopClient.ViewModel.Schedule.Common;

namespace _2X2_Agent.DesktopClient.ViewModel.Common
{
    public static class VmClassLocator
    {
        public const string NameSpaceScheduleViewModel = "_2X2_Agent.DesktopClient.ViewModel.Schedule.Vm";
        public const string NameSpaceScheduleView = "_2X2_Agent.DesktopClient.View.Schedule.Vw";
        public const string NameSpaceTaskView = "_2X2_Agent.DesktopClient.View.Task.Vw";

        public static VmBaseSchedule GetScheduleViewModel(ISchedule schedule)
        {
            var vmScheduleType = Type.GetType(NameSpaceScheduleViewModel + schedule.ShortClassName);
            if (vmScheduleType == null) return null;
            if (vmScheduleType.GetProperty("Model") == null) return null;
            return (VmBaseSchedule)vmScheduleType.GetProperty("Model").GetValue(null);
        }

        public static Window GetView(object o, string nameSpace, string typeView)
        {
            var vwScheduleType = Type.GetType(nameSpace + typeView + o.GetType().Name);
                //schedule.ShortClassName);
            if (vwScheduleType == null) return null;
            return (Window)Activator.CreateInstance(vwScheduleType);
        }
    }
}
