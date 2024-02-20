using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<TaskModel> TaskList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            TaskList = new ObservableCollection<TaskModel>();
            tasksListBox.ItemsSource = TaskList;
            LoadTasksFromJsonFile();
        }

        private void LoadTasksFromJsonFile()
        {
            
        }

        private void SaveTasksToJsonFile()
        {
            
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTask = tasksListBox.SelectedItem as TaskModel;
            if (selectedTask != null)
            {
                selectedTask.Status = "Выполнена";
                selectedTask.CompletionDate = DateTime.Today;
                TaskList.Remove(selectedTask);
                SaveTasksToJsonFile();
            }
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTask = tasksListBox.SelectedItem as TaskModel;
            if (selectedTask != null)
            {
                selectedTask.Status = "Отклонена";
                selectedTask.CompletionDate = DateTime.Today;
                TaskList.Remove(selectedTask);
                SaveTasksToJsonFile();
            }
        }

        private void CreateTaskMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var createTaskWindow = new CreateTaskWindow(TaskList);
            createTaskWindow.ShowDialog();
        }

        private void HistoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new HistoryWindow(TaskList);
            historyWindow.ShowDialog();
        }
    }
    public class TaskModel
    {
        public string Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Today;
        public string Urgency { get; set; }
        public string Status { get; set; } = "Ожидает";
        public DateTime? CompletionDate { get; set; }
    }
}
