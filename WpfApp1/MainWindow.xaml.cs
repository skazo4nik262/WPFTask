using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.ComponentModel;

namespace WpfApp1
{

    public class TaskModel
    {
        public string Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Today;
        public string Urgency { get; set; }
        public string Status { get; set; } = "Ожидает";
        public DateTime? CompletionDate { get; set; }
    }

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<TaskModel> taskList;

        public TaskModel SelectedTask { get; set; }

        public ObservableCollection<TaskModel> TaskList
        {
            get => taskList;
            set
            {
                taskList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TaskList)));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            TaskList = new ObservableCollection<TaskModel>();
            LoadTasksFromJsonFile();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void LoadTasksFromJsonFile()
        {
            if (File.Exists("taskList.json"))
                using (var fs = File.OpenRead("taskList.json"))
                    TaskList = JsonSerializer.Deserialize<ObservableCollection<TaskModel>>(fs);
            else
                TaskList = new ObservableCollection<TaskModel>();
        }

        private void SaveTasksToJsonFile()
        {
            using (var fs = File.Create("taskList.json"))
                JsonSerializer.Serialize(fs, TaskList);
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTask = SelectedTask as TaskModel;
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
            var selectedTask = SelectedTask as TaskModel;
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
            TaskList.Add(createTaskWindow.newTaskModel);
        }

        private void HistoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new HistoryWindow(TaskList);
            historyWindow.ShowDialog();
        }
    }
}
