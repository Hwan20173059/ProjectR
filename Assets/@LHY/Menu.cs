using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //[SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;


    private void Awake()
    {
        //mixer = GetComponent<AudioMixer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("menu를 삭제합니다.");
            Resume();
        }
        //AudioValueUpdate();
    }
    private void Start()
    {
        Debug.Log(PlayerPrefs.GetFloat("Master"));
        masterSlider.value = PlayerPrefs.GetFloat("Master");
        bgmSlider.value = PlayerPrefs.GetFloat("BGM");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX");
        //audiomanager.SettingsSoundData();
        Pause();
    }
    private void Pause()
    {
        Time.timeScale = 0.0f;
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
        Destroy(transform.root.gameObject);
    }

    //todo : 분리
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }




    //-----------audiomixer controls---------------
    //todo : AudioControls.cs로 분리
    public void ToggleMasterVolume_Callback(bool isToggledOn)
    {
        Debug.Log("Asdf");
        string exposedParam = "Master";

        AudioManager.instance.ToggleVolume(exposedParam, isToggledOn);
    }

    public void ToggleBGMVolume_Callback(bool isToggledOn)
    {
        string exposedParam = "BGM";

        AudioManager.instance.ToggleVolume(exposedParam, isToggledOn);
    }

    public void ToggleSFXVolume_Callback(bool isToggledOn)
    {
        string exposedParam = "SFX";

        AudioManager.instance.ToggleVolume(exposedParam, isToggledOn);
    }

    public void SetMasterVolume_Callback()
    {
        float volume = masterSlider.value;
        AudioManager.instance.SetVolume("Master", volume);
    }

    public void SetBGM_Callback()
    {
        float volume = bgmSlider.value;
        AudioManager.instance.SetVolume("BGM", volume);
    }

    public void SetSFXVolume_Callback()
    {
        float volume = sfxSlider.value;
        AudioManager.instance.SetVolume("SFX", volume);
    }





    //ResolutionSettings
    //todo : ResolutionControls.cs 분리 필요
    public void Resolution()
    {

    }
}
