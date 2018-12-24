using System;
using System.ComponentModel;

namespace _2X2_Agent.DesktopClient.ViewModel.Common
{
    public abstract class VmBase :INotifyPropertyChanged, IDisposable
    {
        protected VmBase() { }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChangedEventHandler EventHandler = this.PropertyChanged;
            if (EventHandler != null) EventHandler.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public void Dispose() { OnDispose(); }

        protected virtual void OnDispose()
        {
            //throw new NotImplementedException(); 
        }

    }
}
