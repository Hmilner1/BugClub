using System.Collections.Generic;
using UnityEngine;

public class AudioMan : MonoBehaviour
{
    public static AudioMan instance;

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;

    [SerializeField]
    private List<AudioClip> musicClips;
    [SerializeField]
    private List<AudioClip> SFXClips;

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
        var data = SettingsManager.Instance.LoadData();
        UpdateMusicSource(data.Music);
        UpdateSfxSource(data.SFX);
        PlayMusic(1);
    }

    public void UpdateMusicSource(float volume)
    {
        musicSource.volume= volume;
    }

    public void UpdateSfxSource(float volume)
    {
        sfxSource.volume = volume;
    }

    public void PlayMusic(int musicIndex)
    {
        musicSource.clip = musicClips[musicIndex];
        musicSource.Play();
    }

    public void PlaySfx(int sfxIndex)
    {
        sfxSource.clip = SFXClips[sfxIndex];
        float random = Random.Range(0.9f, 1.1f);
        sfxSource.pitch = random;
        sfxSource.Play();
    }


}
