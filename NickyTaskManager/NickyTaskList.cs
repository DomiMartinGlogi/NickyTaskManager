using System;
using System.Collections.Generic;
using System.Linq;


namespace NickyTaskManager;

public class NickyTaskList
{
    private string name;
    internal List<NickyManagedTask> list;

    public NickyTaskList()
    {
        this.name = "New List";
        this.list = new List<NickyManagedTask>();
    }
    public NickyTaskList(string name)
    {
        this.name = name;
        this.list = new List<NickyManagedTask>();
    }
    public string Name
    {
        get => name;
        set => name = value ?? throw new ArgumentNullException(nameof(value));
    }
    public void addTask(NickyManagedTask task)
    {
        list.Add(task);
        Sort();
    }

    public void removeTask(NickyManagedTask task)
    {
        list.Remove(task);
        Sort();
    }

    public NickyManagedTask currentTask()
    {
        int index = list.Count - 1;
        NickyManagedTask task = list[index];
        return task;
    }

    public void Sort()
    {
        foreach (NickyManagedTask task in list)
        {
            task.calcRealPriority();
        }
        list.OrderByDescending(x => x.realPriority).ToList();
    }
}