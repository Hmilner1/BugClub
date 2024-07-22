using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LocalSaveManager : MonoBehaviour
{
    private static string savePath = Application.persistentDataPath + "/BugData.json";
    private static string SettingssavePath = Application.persistentDataPath + "/SettingsData.json";


    public static void SaveBugData(BugBoxData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    public static BugBoxData LoadBugs()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<BugBoxData>(json);
        }
        return null;
    }


    public static void SaveSettings(SettingsData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SettingssavePath, json);
    }

    public static SettingsData LoadSettings()
    {
        if (File.Exists(SettingssavePath))
        {
            string json = File.ReadAllText(SettingssavePath);
            return JsonUtility.FromJson<SettingsData>(json);
        }
        return null;
    }

}
