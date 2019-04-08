using System;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager
{
    class TaskVM : PropertyChangedBase, ITaskVM
    {
        private readonly int taskDuration;
        private const int NotifyInterval = 1;
        private const int ProgressCompletedValue = 100;
        private const string TaskCompletedMessage = "{0} - Выполнена";

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if(String.Equals(title, value))
                    return;

                title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        private bool isCompleted;
        public bool IsCompleted
        {
            get { return isCompleted; }
            set
            {
                if(isCompleted == value)
                    return;

                isCompleted = value;
                RaisePropertyChanged(nameof(IsCompleted));
            }
        }

        private int progress;
        public int Progress
        {
            get { return progress; }
            set
            {
                if(progress == value)
                    return;

                progress = value;
                RaisePropertyChanged(nameof(Progress));
            }
        }

        public TaskVM(int duration, string title)
        {
            taskDuration = duration;
            Title = title;
        }

        public async void Start()
        {
            if(taskDuration != 0.0m)
            {
                decimal ratio = ProgressCompletedValue / taskDuration;
                foreach(int sec in Enumerable.Range(1, taskDuration))
                {
                    await Task.Delay(TimeSpan.FromSeconds(NotifyInterval));
                    int newValue = (int)(ratio * sec);
                    Progress = newValue > ProgressCompletedValue ? ProgressCompletedValue : newValue;
                }
            }

            EndTask();
        }

        private void EndTask()
        {
            if(Progress < ProgressCompletedValue)
                Progress = ProgressCompletedValue;

            Title = String.Format(TaskCompletedMessage, Title);
            IsCompleted = true;
        }
    }
}