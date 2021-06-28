using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    //Menu de configurações

    //volumes
    public Slider sliderMain;
    public Slider sliderMusic;
    public Slider sliderEffects;
    public float sliderValueMain;
    public float sliderValueMusic;
    public float sliderValueEffects;
    public AudioMixer audioMixer;

    //Tela
    public bool isFullScreen;
    public int qualityTexture;
    public Toggle fullScreenToggle;
    public Dropdown qualityTextureDrop;

    private void OnEnable()
    {
        //CHAMADO DE FUNCÕES
        fullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        qualityTextureDrop.onValueChanged.AddListener(delegate { onTextureQualityChange(); });
    }



    private void Start()
    {
        Screen.fullScreen = fullScreenToggle.isOn;
        QualitySettings.SetQualityLevel(2);
        sliderMain.value = PlayerPrefs.GetFloat("main", sliderValueMain);
        sliderMusic.value = PlayerPrefs.GetFloat("music", sliderValueMusic);
        sliderEffects.value = PlayerPrefs.GetFloat("effect", sliderValueEffects);
        qualityTextureDrop.value = PlayerPrefs.GetInt("quality", qualityTexture);
    }

    //VOLUMES!!
    //volume geral
    public void mainVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume); //nome dado no parametro do mixer de audio

    }

    public void mainVolumeSave(float value)
    {
        sliderValueMain = value;
        PlayerPrefs.SetFloat("main", sliderValueMain);
    }



    //volume da musica
    public void musicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);

    }

    public void musicVolumeSave(float value)
    {
        sliderValueMusic = value;
        PlayerPrefs.SetFloat("music", sliderValueMusic);
    }



    //volume dos efeitos
    public void effectsVolume(float volume)
    {
        audioMixer.SetFloat("effectsVolume", volume);
    }

    public void effectVolumeSave(float value)
    {
        sliderValueEffects = value;
        PlayerPrefs.SetFloat("effect", sliderValueEffects);
    }

    //RESOLUÇÃO


    public void OnFullScreenToggle()
    {
        Screen.fullScreen = fullScreenToggle.isOn;
        //onResolutionChange();
    }

    public void onTextureQualityChange()
    {
        QualitySettings.SetQualityLevel(qualityTextureDrop.value);
    }

    public void saveQuality()
    {
        PlayerPrefs.SetInt("quality", qualityTextureDrop.value);
    }
    

}
