using System;

namespace NickyTaskManager;

public class NickyManagedTask
{
    private string task;
    private Priority priority;
    private DateTime endDate;
    private DateTime startDate;
    internal int realPriority;

    public NickyManagedTask(string task, Priority priority, DateTime endDate)
    {
        this.task = task;
        this.priority = priority;
        this.startDate = DateTime.Now;
        this.endDate = endDate;
        calcRealPriority();
    }

    public DateTime StartDate
    {
        get => startDate;
        set => startDate = value;
    }

    public DateTime EndDate
    {
        get => endDate;
        set => endDate = value;
    }

    public Priority Priority
    {
        get => priority;
        set => priority = value;
    }

    public string Task
    {
        get => task;
        set => task = value ?? throw new ArgumentNullException(nameof(value));
    }

    public TimeSpan getTimeLeft()
    {
        return endDate - DateTime.Now;;
    }

    public void calcRealPriority()
    {
        this.realPriority = (int)(((byte)Priority) * (getTimeLeft().Hours));
    }
}