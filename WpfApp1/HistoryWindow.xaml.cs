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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        private ObservableCollection<TaskModel> TaskList { get; set; }

        public HistoryWindow(ObservableCollection<TaskModel> taskList)
        {
            InitializeComponent();
            TaskList = taskList;
            FilterHistoryTasks();
        }

        // Фильтрация задач для отображения истории
        private void FilterHistoryTasks()
        {
            var historyTasks = TaskList.Where(t => t.Status == "Выполнена" || t.Status == "Отклонена");
            historyListBox.ItemsSource = historyTasks;
        }
    }
}
