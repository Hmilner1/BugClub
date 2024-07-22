using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    private SettingsData data;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadData();
    }

    public SettingsData LoadData()
    {
        data = LocalSaveManager.LoadSettings();
        if (data == null)
        {
            data = new SettingsData(true, 0.5f, 0.25f, 1f);
            SetAndSaveSettings(data);
            return data;
        }
        return data;
    }

    public void SetAndSaveSettings(SettingsData settingData)
    {
        data = settingData;
        LocalSaveManager.SaveSettings(data);
        EventManager.instance.SettingsUpdated();
    }


}
