using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace NickyTaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<NickyTaskList> list;
        private List<String> nameListTaskLists;
        private List<String> nameListTasks;
        private List<String> priority;
        private int indexOfList;
        private int indexOfTask;
        private DispatcherTimer dispatcherTimer;
        public MainWindow()
        {
            //  DispatcherTimer setup
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0,0,1);
            dispatcherTimer.Start();
            InitializeComponent();
            Window_Loaded();

        }

        private void Window_Loaded()
        {
            priority = new List<string>();
            priority.Add("Very High");
            priority.Add("High");
            priority.Add("Medium");
            priority.Add("Low");
            priority.Add("Very Low");
            Priority.ItemsSource = priority;
            list = Utility.launcher();
            Update();
        }

        //Update Method, Should be called after EVERY function
        private void Update()
        {
            /*
             * I believe that this method, at some point
             * Breaks something in the display of the Task selection combobox,
             * I do not why that is, but it displays Tasks from all lists
             */
            nameListTasks = new List<string>();
            nameListTaskLists = new List<string>();
            NickyTaskList tempTaskList;
            foreach (NickyTaskList subList in list)
            {
                nameListTaskLists.Add(subList.Name);
            }
            ListSelect.ItemsSource = nameListTaskLists;
            int index = -1;
            foreach (NickyTaskList sublist in list)
            {
                if (sublist.Name == ListSelect.Text)
                {
                    foreach (var task in sublist.list)
                    {
                        nameListTasks.Add(task.Task);
                    }
                    indexOfTask = list.IndexOf(sublist);
                    break;
                }
            }
            
            TaskSelect.ItemsSource = nameListTasks;
        }

        //Saving
        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            Utility.saver(list);
            Update();
        }

        private void CreateList_OnClick(object sender, RoutedEventArgs e)
        {
            if (New_List.Text == "" || New_List.Text == "New List")
            {
                
            }
            else
            {
                NickyTaskList newList = new NickyTaskList(New_List.Text);
                list.Add(newList);
                New_List.Text = "New List";
            }
            Update();
        }
        
        //Method to Remove a List from the List of Tasklists.
        private void RemoveListButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ListSelect.Text == "")
            {
                
            }
            else
            {
                foreach (NickyTaskList subList in list)
                {
                    if (subList.Name == ListSelect.Text)
                    {
                        list.Remove(subList);
                        break;
                    }
                }
            }
            Update();
        }
        
        //Method to change the Task List upon choosing a different TaskList.
        private void ListSelect_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var nameOfList = ListSelect.Text;
            NickyTaskList listChosen = new NickyTaskList();
            foreach (var sublist in list)
            {
                if (nameOfList == sublist.Name)
                {
                    listChosen = sublist;
                }
            }
            indexOfList = list.IndexOf(listChosen);
            Update();
        }

        //Method to Create A Task.
        private void CreateTask_OnClick(object sender, RoutedEventArgs e)
        {
            if (New_Task.Text == "" || New_Task.Text == "New Task")
            {
                Console.WriteLine("Invalid Task Name");
                return;
            }

            if (DueDateNew.SelectedDate < DateTime.Now)
            {
                Console.WriteLine("Date cannot be in the past");
                return;
            }

            if (indexOfList > list.Count - 1 || indexOfList < 0 || indexOfList == null)
            {
                Console.WriteLine("Index out of Bounds");
                if (indexOfList == null)
                {
                    Console.WriteLine("Index is null");
                }
                else
                {
                    Console.WriteLine("Index is: " + indexOfList);
                }
                return;
            }
            
            Priority priority = Priority.Text switch
            {
                "Very High" => NickyTaskManager.Priority.VeryHigh,
                "High" => NickyTaskManager.Priority.High,
                "Medium" => NickyTaskManager.Priority.Medium,
                "Low" => NickyTaskManager.Priority.Low,
                "Very Low" => NickyTaskManager.Priority.VeryLow,
                _ => NickyTaskManager.Priority.Default
            };
            NickyManagedTask newTask = new NickyManagedTask(New_Task.Text, priority, (DateTime)DueDateNew.SelectedDate);
            NickyTaskList taskList = list[indexOfList];
            taskList.addTask(newTask);
            Update();
            New_Task.Text = "New Task";
        }
        
        //Updates the Screen every Second
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Update();
        }
        
        //Removes a Task from the Tasklist
        private void TaskRemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            NickyTaskList tempList = list[indexOfList];
            list.Remove(tempList);
            foreach (var task in tempList.list)
            {
                if (task.Task == TaskSelect.Text)
                {
                    tempList.list.Remove(task);
                    break;
                }
                Console.WriteLine("Task not found.");
            }
            list.Add(tempList);
            Update();
        }
    }
}