using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Windows;
using System.Windows.Controls;

namespace NickyTaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<NickyTaskList> list;
        private List<String> nameList;
        private List<String> priority;
        private int indexOfList;
        private int indexOfTask;
        public MainWindow()
        {
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

        private void Update()
        {
            nameList = new List<string>();
            foreach (NickyTaskList subList in list)
            {
                nameList.Add(subList.Name);
            }
            ListSelect.ItemsSource = nameList;
            int index = -1;
            foreach (NickyTaskList sublist in list)
            {
                if (sublist.Name == ListSelect.Text)
                {
                    index = list.IndexOf(sublist);
                    break;
                }
            }
        }

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

        private void ListSelect_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                NickyTaskList taskList = list[indexOfList];
                List<String> taskNames = new List<string>();
                foreach (NickyManagedTask task in taskList.list)
                {
                    taskNames.Add(task.Task);
                }
                ItemSelect.ItemsSource = taskNames;
            }
            catch (Exception exception)
            {
                Console.WriteLine("List not found.");
            }
        }
    }
}