using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace TaskManager
{
    class TaskManagerVM : PropertyChangedBase, IDataErrorInfo
    {
        private const string TaskDurationNotSetMessage = "Не задана продолжительность задачи";
        private const string TaskDurationInvalidFormatMessage = "Неверный формат";
        private const string NegativeTaskDurationMessage = "Продолжительность задачи не может быть отрицательной";
        private const string NoTaskTitleMessage = "Название задачи отсутствует";
        public ObservableCollection<ITaskVM> TaskList { get; set; }
        private ICollectionView taskListView;

        public TaskManagerVM()
        {
            TaskList = new ObservableCollection<ITaskVM>();
            taskListView = CollectionViewSource.GetDefaultView(TaskList);

            TaskList.CollectionChanged += (sender, args) =>
            {
                if(args.NewItems != null)
                    foreach(ITaskVM item in args.NewItems)
                        item.PropertyChanged += TaskPropertyChanged;

                if(args.OldItems != null)
                    foreach(ITaskVM item in args.OldItems)
                        item.PropertyChanged -= TaskPropertyChanged;
            };
        }

        private void TaskPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(TaskVM.Progress))
                SortData();
        }

        private TaskVM selectedTask;
        public TaskVM SelectedTask
        {
            get { return selectedTask; }
            set
            {
                if(selectedTask == value)
                    return;

                selectedTask = value;
                RaisePropertyChanged(nameof(SelectedTask));
            }
        }

        #region add task command
        private string taskTitle;
        public string TaskTitle
        {
            get { return taskTitle; }
            set
            {
                if(String.Equals(taskTitle, value))
                    return;

                taskTitle = value;
                RaisePropertyChanged(nameof(TaskTitle));
                RaisePropertyChanged(nameof(TaskTitleError));
            }
        }

        private int taskDurationValue;

        private string taskDuration;
        public string TaskDuration
        {
            get { return taskDuration; }
            set
            {
                if(String.Equals(taskDuration, value))
                    return;

                taskDuration = value;
                RaisePropertyChanged(nameof(TaskDuration));
                RaisePropertyChanged(nameof(TaskDurationError));
            }
        }

        private ICommand addTaskCommand;
        public ICommand AddTaskCommand => addTaskCommand ?? (addTaskCommand = new Command(x => CanAddTask(), x => AddTask()));

        private void AddTask()
        {
            TaskVM task = new TaskVM(taskDurationValue, TaskTitle);
            TaskList.Add(task);
            task.Start();
        }

        private bool CanAddTask() => taskDurationValue >= 0 && !String.IsNullOrWhiteSpace(TaskTitle);
        #endregion

        #region close task command
        private ICommand closeCommand;
        public ICommand CloseCommand => closeCommand ?? (closeCommand = new Command(x => CanClose(), x => Close()));

        public void Close()
        {
            TaskList.Remove(SelectedTask);
        }

        private bool CanClose()
        {
            return SelectedTask != null && TaskList.Contains(SelectedTask);
        }
        #endregion

        private void SortData()
        {
            ListSortDirection direction = ListSortDirection.Ascending;

            using(taskListView.DeferRefresh())
            {
                taskListView.SortDescriptions.Clear();
                taskListView.SortDescriptions.Add(new SortDescription(nameof(TaskVM.Progress), direction));
            }
        }

        #region IDataErrorInfo
        public string Error => null;

        public string TaskDurationError => CheckTaskDuration();
        public string TaskTitleError => CheckTaskTitle();


        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case nameof(TaskDuration):
                        return CheckTaskDuration();

                    case nameof(TaskTitle):
                        return CheckTaskTitle();

                    default:
                        return null;
                }
            }
        }

        private string CheckTaskDuration()
        {
            if(String.IsNullOrWhiteSpace(TaskDuration))
                return TaskDurationNotSetMessage;

            if(!Int32.TryParse(TaskDuration, out taskDurationValue))
                return TaskDurationInvalidFormatMessage;

            if(taskDurationValue < 0.0m)
                return NegativeTaskDurationMessage;

            return null;
        }

        private string CheckTaskTitle()
        {
            if(String.IsNullOrWhiteSpace(TaskTitle))
                return NoTaskTitleMessage;

            return null;
        }
        #endregion
    }
}