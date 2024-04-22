using Assets.PixelFantasy.PixelTileEngine.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    //[SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private AutoBattleSettingPanel autoBattleSettingPanel;

    private void Awake()
    {
        autoBattleSettingPanel.Init();
        //mixer = GetComponent<AudioMixer>();
    }
    private void Update()
    {

    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("Master"))
            masterSlider.value = PlayerPrefs.GetFloat("Master");
        if (PlayerPrefs.HasKey("BGM"))
            bgmSlider.value = PlayerPrefs.GetFloat("BGM");
        if (PlayerPrefs.HasKey("SFX"))
            sfxSlider.value = PlayerPrefs.GetFloat("SFX");
    }

    public void ToggleMasterVolume_Callback(bool isToggledOn)
    {
        string exposedParam = "Master";

        AudioManager.Instance.ToggleVolume(exposedParam, isToggledOn);
    }

    public void ToggleBGMVolume_Callback(bool isToggledOn)
    {
        string exposedParam = "BGM";

        AudioManager.Instance.ToggleVolume(exposedParam, isToggledOn);
    }

    public void ToggleSFXVolume_Callback(bool isToggledOn)
    {
        string exposedParam = "SFX";

        AudioManager.Instance.ToggleVolume(exposedParam, isToggledOn);
    }

    public void SetMasterVolume_Callback()
    {
        float volume = masterSlider.value;
        AudioManager.Instance.SetVolume("Master", volume);
    }

    public void SetBGMVolmue_Callback()
    {
        float volume = bgmSlider.value;
        AudioManager.Instance.SetVolume("BGM", volume);
    }

    public void SetSFXVolume_Callback()
    {
        float volume = sfxSlider.value;
        AudioManager.Instance.SetVolume("SFX", volume);
    }





    public void Escape()
    {
       
        if (PlayerManager.Instance.firstGame == true)
        {
            PlayerManager.Instance.townUiManager.tutorialUI.gameObject.SetActive(true);
            PlayerManager.Instance.townUiManager.tutorialUI.ActiveTutorial(0);

            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
