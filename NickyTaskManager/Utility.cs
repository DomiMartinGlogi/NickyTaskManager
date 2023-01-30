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
        try
        {
            var jsonString = File.ReadAllText(fileName);
            list = JsonSerializer.Deserialize<List<NickyTaskList>>(jsonString);
        }
        catch (Exception e)
        {
            Console.WriteLine("A list wasn't found, a new list will be generated.");
            list = new List<NickyTaskList>();
        }

        foreach (var sublist in list)
        {
            var filename = (sublist.Name + ".json");
            try
            {
                var jsonStringSublist = File.ReadAllText(filename);
                sublist.list = JsonSerializer.Deserialize<List<NickyManagedTask>>(jsonStringSublist);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while loading the sublist " + sublist.Name + ": " + e.Message);
                sublist.list = new List<NickyManagedTask>();
            }
        }

        return list;
    }

    
    internal static void saver(List<NickyTaskList> list)
    {
        var fileName = "Lists.json";
        var jsonString = JsonSerializer.Serialize(list);
        File.WriteAllText(fileName, jsonString);

        foreach (var sublist in list)
        {
            var fileNameSublist = (sublist.Name + ".json");
            var jsonStringSublist = JsonSerializer.Serialize(sublist.list);
            File.WriteAllText(fileNameSublist, jsonStringSublist);
        }
    }

}
