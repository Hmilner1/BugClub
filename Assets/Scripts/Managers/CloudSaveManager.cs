using Newtonsoft.Json;
using System.Collections.Generic;
using Unity.Services.CloudSave;
using UnityEngine;

public class CloudSaveManager : MonoBehaviour
{
    public static CloudSaveManager instance { get; private set; }

    private bool playerLoggedIn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerLoggedIn = AccountManager.instance.SignedIn();
    }

    public async void LoadBugSave()
    {
        var BugData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "Bugs" });
        if (BugData.TryGetValue("Bugs", out var keyName))
        {
            string json = keyName.Value.GetAsString();
            BugBox.instance.bugData = JsonUtility.FromJson<BugBoxData>(json);
        }
        else
        {
            BugBox.instance.bugData = null;
        }
    }

    public async void SaveBugData(BugBoxData BugData)
    {
        var data = new Dictionary<string, object> { { "Bugs", BugData } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
    }


}
