using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    private SettingsData data;
    [SerializeField]
    private GameObject audioManager;


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

        Instantiate(audioManager);

    }

    public SettingsData LoadData()
    {
        data = LocalSaveManager.LoadSettings();
        if (data == null)
        {
            data = new SettingsData(true, 0.5f, 0.25f, 144, 1f);
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
