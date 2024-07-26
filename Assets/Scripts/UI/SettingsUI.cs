using TMPro;
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
    private Slider FPS;
    private int fpsValue;
    [SerializeField]
    private Slider battleSpeed;
    private float battleSpeedValue;

    [SerializeField]
    TMP_Text fpsText;

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
        fpsValue = data.FPS;
        battleSpeedValue = data.BattleSpeed;
    }

    private void UpdateUI()
    { 
        screenContols.isOn = sceenControlsEnabled;
        SFX.value = sfxValue;
        Music.value = musicValue;
        FPS.value = fpsValue;
        battleSpeed.value = battleSpeedValue;
        int fpsInt = (int)FPS.value;
        fpsText.text = fpsInt.ToString();
    }

    public void UpdateDataValues()
    { 
        data.OnScreenControls = screenContols.isOn;
        data.SFX = SFX.value;
        data.Music = Music.value;
        data.FPS = (int)FPS.value;
        data.BattleSpeed = battleSpeed.value;

        SettingsManager.Instance.SetAndSaveSettings(data);
    }

    public void OnFpsChanged()
    {
        int fpsInt = (int)FPS.value;
        fpsText.text = fpsInt.ToString();
        Application.targetFrameRate = (int)FPS.value;
    }

    public void OnMusicChanged()
    {
        AudioMan.instance.UpdateMusicSource(Music.value);
    }

    public void OnSfxChanged()
    { 
        AudioMan.instance.UpdateSfxSource(SFX.value);
    }
}
