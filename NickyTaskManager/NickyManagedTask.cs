using System;

namespace NickyTaskManager;

public class NickyManagedTask
{
    private string task;
    /*
     * Priority is to be between 0 - 255
     * Only 0-5 are to be used
     * 0 - None
     * 1 - Very Low
     * 2 - Low
     * 3 - Medium
     * 4 - High
     * 5 - Very High
     *
     * Custom Priorities are to be set to be between 100 - 200.
     */
    private byte priority;
    private DateTime endDate;
    private DateTime startDate;

    public NickyManagedTask(string task, byte priority, DateTime endDate)
    {
        this.task = task;
        this.priority = priority;
        this.startDate = DateTime.Now;
        this.endDate = endDate;
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

    public byte Priority
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
}