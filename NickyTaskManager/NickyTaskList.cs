using System.Collections.Generic;
using System.Threading.Tasks;

namespace NickyTaskManager;

public class NickyTaskList
{
    private List<NickyManagedTask> list;

    public NickyTaskList()
    {
        this.list = new List<NickyManagedTask>();
    }

    public void addTask(NickyManagedTask task)
    {
        list.Add(task);
    }

    public void removeTask(NickyManagedTask task)
    {
        list.Remove(task);
    }

    public NickyManagedTask currentTask()
    {
        int index = list.Count - 1;
        NickyManagedTask task = list[index];
        return task;
    }
    
}