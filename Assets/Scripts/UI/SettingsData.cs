using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public bool OnScreenControls;
    public float SFX;
    public float Music;
    public int FPS;
    public float BattleSpeed;

    public SettingsData(bool screenControls, float SFXVol, float MusicVol ,int fpsValue,  float battleSpeed)
    {
        OnScreenControls= screenControls;
        SFX = SFXVol;
        Music = MusicVol;
        FPS= fpsValue;
        BattleSpeed = battleSpeed;
    }
}
