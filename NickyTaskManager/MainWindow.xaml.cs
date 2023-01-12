using System;
using System.Collections.Generic;
using System.Windows;

namespace NickyTaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<NickyTaskList> list;
        private List<String> nameList;
        public MainWindow()
        {
            InitializeComponent();
            Window_Loaded();

        }

        private void Window_Loaded()
        {
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
    }
}