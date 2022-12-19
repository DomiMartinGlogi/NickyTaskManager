using System.Collections.Generic;
using System.Linq;
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
        list.OrderByDescending(x => x.realPriority).ToList();
    }
}