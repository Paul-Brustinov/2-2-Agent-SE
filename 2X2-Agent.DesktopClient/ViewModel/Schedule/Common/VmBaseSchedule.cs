using _2X2_Agent.DesktopClient.ViewModel.Common;

namespace _2X2_Agent.DesktopClient.ViewModel.Schedule.Common
{
    public class VmBaseSchedule : VmBase
    {
        private static VmBaseSchedule _model;
        public static VmBaseSchedule Model => _model;

        public VmBaseSchedule()
        {
            _model = this;
        }
    }
}
