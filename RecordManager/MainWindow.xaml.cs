using System.Windows;

namespace TaskManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new TaskManagerVM();
        }
    }
}