using System;
using System.ComponentModel;

namespace TaskManager
{
    interface ITaskVM : INotifyPropertyChanged
    {
        int Progress { get; set; }
        string Title { get; set; }
        void Start();
        bool IsCompleted { get; set; }
    }
}