using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource bgmSource;
    public AudioSource sfxCource;
    public AudioMixer audioMixer;
    public Audios audios;

    private readonly Dictionary<string, float> mixerVolume = new();
    private readonly Dictionary<string, bool> mixerMuteToggle = new();

    public CurrentState playerState;

    //public float bgmVolumeScale, sfxVolumeScale, masterVolumeScale;



    public void SetState()
    {
        playerState = PlayerManager.Instance.currentState;
        ChangeBGM();
    }


    
    public void PlaySFX(AudioClip clip)
    {
        sfxCource.PlayOneShot(clip);
    }

    public void PlayLevelUpSFX()
    {
        sfxCource.PlayOneShot(audios.levelupClip);
    }

    public void PlayUISelectSFX()
    {
        sfxCource.PlayOneShot(audios.uiSelectClip);
    }

    public void ChangeBGM()
    {
        switch (playerState)
        {
            case CurrentState.town1:
                bgmSource.clip = audios.townBGMClip;
                break;

            case CurrentState.town2:
                bgmSource.clip = audios.townBGMClip;
                break;

            case CurrentState.title:
                bgmSource.clip = audios.titleBGMClip;
                break;

            case CurrentState.dungeon1:
                bgmSource.clip = audios.dungeonBattleBGMClip;
                break;

            case CurrentState.dungeon2:
                bgmSource.clip = audios.dungeonBattleBGMClip;
                break;

            case CurrentState.dungeon3:
                bgmSource.clip = audios.dungeonBattleBGMClip;
                break;

            case CurrentState.dungeon4:
                bgmSource.clip = audios.dungeonBattleBGMClip;
                break;

            case CurrentState.field:
                bgmSource.clip = audios.fieldBGMClip;
                break;
        }
        bgmSource.Play();
    }

    private void Start()
    {
        SettingsSoundData();
        SetState();
    }

    private void Update()
    {
        playerState = PlayerManager.Instance.currentState;
    }

    public void SettingsSoundData()
    {
        SetVolume("Master", PlayerPrefs.GetFloat("Master"));
        SetVolume("BGM", PlayerPrefs.GetFloat("BGM"));
        SetVolume("SFX", PlayerPrefs.GetFloat("SFX"));
    }

    public void ToggleVolume(string exposedParam, bool isToggledOn)
    {

        if (isToggledOn)
        {
            if (!mixerVolume.ContainsKey(exposedParam))
            {
                return;
            }

            float volume = mixerVolume[exposedParam];
            audioMixer.SetFloat(exposedParam, volume);
            PlayerPrefs.SetFloat(exposedParam, volume);
        }
        else
        {
            audioMixer.GetFloat(exposedParam, out float temp);
            mixerVolume[exposedParam] = temp;
            audioMixer.SetFloat(exposedParam, -80f);
        }
    }
    public void SetVolume(string mixerParam, float volume)
    {
        if (!mixerMuteToggle.ContainsKey(mixerParam))
        {
            mixerMuteToggle[mixerParam] = true;
        }

        bool isToggledOn = mixerMuteToggle[mixerParam];
        if (isToggledOn)
        {
            float result = CalculateMixerVolume(volume);
            audioMixer.SetFloat(mixerParam, result);
            PlayerPrefs.SetFloat(mixerParam, volume);
        }
        else
        {
            mixerVolume[mixerParam] = CalculateMixerVolume(volume);
        }

        //소리 표준화(slider = 1 ~ 0dB -> audiomixergroup = 0 ~ -50dB)
        float CalculateMixerVolume(float f)
        {
            return Mathf.Log10(f) * 20;
        }

    }
}

