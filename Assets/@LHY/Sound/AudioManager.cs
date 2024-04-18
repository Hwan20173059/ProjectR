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

    public void PlayAtkGuardSFX()
    {
        sfxCource.PlayOneShot(audios.atkGuardClip);
    }
    public void PlayAtkMagicSFX()
    {
        sfxCource.PlayOneShot(audios.atkMagicClip);
    }
    public void PlayAtkRangeHitSFX()
    {
        sfxCource.PlayOneShot(audios.atkRangeHitClip);
    }
    public void PlayAttack1SFX()
    {
        sfxCource.PlayOneShot(audios.attack1Clip);
    }
    public void PlayAttack2SFX()
    {
        sfxCource.PlayOneShot(audios.attack2Clip);
    }
    public void PlayBaseAttackSFX()
    {
        sfxCource.PlayOneShot(audios.baseAttackClip);
    }
    public void PlayBattleLoseSFX()
    {
        sfxCource.PlayOneShot(audios.battleLoseClip);
    }
    public void PlayBuySFX()
    {
        sfxCource.PlayOneShot(audios.buyClip);
    }
    public void PlayEquipSFX()
    {
        sfxCource.PlayOneShot(audios.equipClip);
    }
    public void PlayHit1SFX()
    {
        sfxCource.PlayOneShot(audios.hit1Clip);
    }
    public void PlayHit2SFX()
    {
        sfxCource.PlayOneShot(audios.hit2Clip);
    }
    public void PlayMonsterAttackSFX()
    {
        sfxCource.PlayOneShot(audios.monsterAttackClip);
    }
    public void PlayMonsterDeathSFX()
    {
        sfxCource.PlayOneShot(audios.monsterDeathClip);
    }
    public void PlayMonsterJumpSFX()
    {
        sfxCource.PlayOneShot(audios.monsterJumpClip);
    }
    public void PlayPauseSFX()
    {
        sfxCource.PlayOneShot(audios.pauseClip);
    }
    public void PlayRewardSFX()
    {
        sfxCource.PlayOneShot(audios.rewardClip);
    }
    public void PlayUnEquipSFX()
    {
        sfxCource.PlayOneShot(audios.unEquipClip);
    }
    public void PlayUnPauseSFX()
    {
        sfxCource.PlayOneShot(audios.unPauseClip);
    }
    public void PlayUseItemSFX()
    {
        sfxCource.PlayOneShot(audios.useItemClip);
    }
    public void ChangeBGM()
    {
        switch (playerState)
        {
            case CurrentState.town1:
                bgmSource.clip = audios.town1Clip;
                break;

            case CurrentState.town2:
                bgmSource.clip = audios.town2Clip;
                break;

            case CurrentState.title:
                bgmSource.clip = audios.titleClip;
                break;

            case CurrentState.dungeon1:
                bgmSource.clip = audios.forestDungeonClip;
                break;

            case CurrentState.dungeon2:
                bgmSource.clip = audios.ruinDungeonClip;
                break;

            case CurrentState.dungeon3:
                bgmSource.clip = audios.oceanDungeonClip;
                break;

            case CurrentState.dungeon4:
                bgmSource.clip = audios.hellDungeonClip;
                break;

            case CurrentState.field:
                bgmSource.clip = audios.fieldClip;
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

