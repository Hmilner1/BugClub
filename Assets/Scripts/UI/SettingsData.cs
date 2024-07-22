using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public bool OnScreenControls;
    public float SFX;
    public float Music;
    public float BattleSpeed;

    public SettingsData(bool screenControls, float SFXVol, float MusicVol, float battleSpeed)
    {
        OnScreenControls= screenControls;
        SFX = SFXVol;
        Music = MusicVol;
        BattleSpeed = battleSpeed;
    }
}
