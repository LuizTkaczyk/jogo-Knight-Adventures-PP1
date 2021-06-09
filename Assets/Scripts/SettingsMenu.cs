using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    //volumes
    public Slider sliderMain;
    public Slider sliderMusic;
    public Slider sliderEffects;
    public float sliderValueMain;
    public float sliderValueMusic;
    public float sliderValueEffects;
    public AudioMixer audioMixer;

    //resolução
    public bool isFullScreen;
    public int resolutionIndex;
    public int qualityTexture;
    public Toggle fullScreenToggle;
    public Dropdown resolutionDrop;
    public Dropdown qualityTextureDrop;

    public Resolution[] resolutions;

    private void OnEnable()
    {
        resolutions = Screen.resolutions;
        foreach (Resolution reso in resolutions)
        {
            resolutionDrop.options.Add(new Dropdown.OptionData(reso.ToString()));
        }

        //CHAMADO DE FUNCÕES
        fullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        resolutionDrop.onValueChanged.AddListener(delegate { onResolutionChange(); });
        qualityTextureDrop.onValueChanged.AddListener(delegate { onTextureQualityChange(); });
    }



    private void Start()
    {

        
        sliderMain.value = PlayerPrefs.GetFloat("main", sliderValueMain);
        sliderMusic.value = PlayerPrefs.GetFloat("music", sliderValueMusic);
        sliderEffects.value = PlayerPrefs.GetFloat("effect", sliderValueEffects);

        resolutionDrop.value = PlayerPrefs.GetInt("reso", resolutionIndex);
        qualityTextureDrop.value = PlayerPrefs.GetInt("quality", qualityTexture);

        if ((PlayerPrefs.GetInt("toggle") == 1))
        {
            fullScreenToggle.isOn = true;
        }
        else
        {
            fullScreenToggle.isOn = false;
        }
       
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
        onResolutionChange();
    }

    public void saveFullScreen()
    {
        if(fullScreenToggle.isOn == true)
        {
            PlayerPrefs.SetInt("toggle", 1);
        }
        else
        {
            PlayerPrefs.SetInt("toggle", 0);
        }

    }

    public void onResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDrop.value].width, resolutions[resolutionDrop.value].height, fullScreenToggle.isOn);
        
    }

    public void saveResolution()
    {
        PlayerPrefs.SetInt("reso", resolutionDrop.value);
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
