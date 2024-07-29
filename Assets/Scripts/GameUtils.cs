using UnityEngine;
using File = System.IO.File;

public static class GameUtils
{
    public static void Print(string message)
    {
        Debug.Log(message);
    }

    public static void SaveData(object data)
    {
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public static T LoadData<T>()
    {
        var path = Application.persistentDataPath + "/savefile.json";
        if (!File.Exists(path)) return default;

        var json = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(json);
    }
}
