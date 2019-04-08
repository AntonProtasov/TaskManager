using System.ComponentModel;

namespace TaskManager
{
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string pname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pname));
        }
    }
}