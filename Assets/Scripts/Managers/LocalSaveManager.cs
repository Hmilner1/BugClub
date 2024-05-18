using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LocalSaveManager : MonoBehaviour
{
    private static string savePath = Application.persistentDataPath + "/BugData.json";

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


}
