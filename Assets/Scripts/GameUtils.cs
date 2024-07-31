using System.IO;
using UnityEngine;
using File = System.IO.File;

public class GameUtils : MonoBehaviour
{
    public static void Print(string message)
    {
        Debug.Log(message);
    }

    public static void SaveData<T>(T data)
    {
        Print("Saving Data...");
        var json = JsonUtility.ToJson(data);
        var path = Path.Combine(Application.persistentDataPath, "savefile.json");
        File.WriteAllText(path, json);
        Print("Saved " + json + " to " + path + " successfully!");
    }

    public static T LoadData<T>()
    {
        Print("Loading Data...");
        var path = Path.Combine(Application.persistentDataPath, "savefile.json");
        if (!File.Exists(path)) return default;

        var json = File.ReadAllText(path);
        Print("Loaded " + json + " from " + path + " successfully!");
        return JsonUtility.FromJson<T>(json);
    }
}