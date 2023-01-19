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
        List<NickyTaskList> list;
        List<NickyTaskList> completeList = new List<NickyTaskList>();
        try
        {
            var jsonString = File.ReadAllText(fileName);
            list = JsonSerializer.Deserialize<List<NickyTaskList>>(jsonString);
        }
        catch (Exception e)
        {
            Console.WriteLine("A list wasn't found, a new list will be generated.");
            list = new List<NickyTaskList>();
            return list;
        }

        try
        {
            foreach (var sublist in list)
            {
                var filename = (sublist.Name + ".json");
                NickyTaskList newSublist = JsonSerializer.Deserialize<NickyTaskList>(fileName);
                completeList.Add(newSublist);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            completeList = new List<NickyTaskList>();
        }

        return completeList;
    }

    internal static void saver(List<NickyTaskList> list)
    {
        var jsonString = JsonSerializer.Serialize(list);
        var fileName = "Lists.json";
        File.WriteAllText(fileName,jsonString);
        foreach (var sublist in list)
        {
            var fileNameSublist = (sublist.Name + ".json");
            var jsonStringSublist = JsonSerializer.Serialize(sublist.list);
            File.WriteAllText(fileNameSublist,jsonStringSublist);
        }
    }
}
