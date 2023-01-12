using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace NickyTaskManager;

public static class Utility
{   
    internal static List<NickyTaskList> launcher()
    {
        var fileName = "Lists.json";
        try
        {
            var jsonString = File.ReadAllText(fileName);
            List<NickyTaskList> list = JsonSerializer.Deserialize<List<NickyTaskList>>(jsonString);
            return list;
        }
        catch (Exception e)
        {
            Console.WriteLine("A list wasn't found, a new list will be generated.");
            List<NickyTaskList> list = new List<NickyTaskList>();
            return list;
        }
    }

    internal static void saver(List<NickyTaskList> list)
    {
        var jsonString = JsonSerializer.Serialize(list);
        var fileName = "Lists.json";
        File.WriteAllText(fileName,jsonString);
    }
}