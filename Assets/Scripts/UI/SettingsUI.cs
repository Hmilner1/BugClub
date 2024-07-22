using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    private SettingsData data;

    [SerializeField]
    private Toggle screenContols;
    private bool sceenControlsEnabled;
    [SerializeField]
    private Slider SFX;
    private float sfxValue;
    [SerializeField]
    private Slider Music;
    private float musicValue;
    [SerializeField]
    private Slider battleSpeed;
    private float battleSpeedValue;

    private void Start()
    {
        updateValues();
        UpdateUI();
    }

    private void updateValues()
    {
        data = SettingsManager.Instance.LoadData();

        sceenControlsEnabled = data.OnScreenControls;
        sfxValue = data.SFX;
        musicValue = data.Music;
        battleSpeedValue = data.BattleSpeed;
    }

    private void UpdateUI()
    { 
        screenContols.isOn = sceenControlsEnabled;
        SFX.value = sfxValue;
        Music.value = musicValue;
        battleSpeed.value = battleSpeedValue;
    }

    public void UpdateDataValues()
    { 
        data.OnScreenControls = screenContols.isOn;
        data.SFX = SFX.value;
        data.Music = Music.value;
        data.BattleSpeed = battleSpeed.value;

        SettingsManager.Instance.SetAndSaveSettings(data);
    }
}
