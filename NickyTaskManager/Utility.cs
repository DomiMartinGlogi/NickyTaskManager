using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace NickyTaskManager;

public static class Utility
{   
    internal static List<NickyTaskList> launcher()
    {
        var fileName = "Lists.json";
        var jsonString = File.ReadAllText(fileName);
        List<NickyTaskList> list = JsonSerializer.Deserialize<List<NickyTaskList>>(jsonString);
        return list;
    }

    internal static void saver(List<NickyTaskList> list)
    {
        var jsonString = JsonSerializer.Serialize(list);
        var fileName = "Lists.json";
        File.WriteAllText(fileName,jsonString);
    }
}