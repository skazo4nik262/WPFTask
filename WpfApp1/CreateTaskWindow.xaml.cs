﻿using System;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для CreateTaskWindow.xaml
    /// </summary>
    public partial class CreateTaskWindow : Window
    {
        private ObservableCollection<TaskModel> TaskList { get; set; }

        public CreateTaskWindow(ObservableCollection<TaskModel> taskList)
        {
            InitializeComponent();
            TaskList = taskList;
        }

        public TaskModel newTaskModel { get; set; }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var newTask = new TaskModel
            {
                Description = descriptionTextBox.Text,
                Urgency = urgencyComboBox.SelectedItem?.ToString()
            };
            newTaskModel = newTask;
            TaskList.Add(newTask);
            SaveTasksToJsonFile();
            Close();
        }
        private void SaveTasksToJsonFile()
        {
            using (var fs = File.Create("taskList.json"))
                JsonSerializer.Serialize(fs, TaskList);
        }
    }
}
