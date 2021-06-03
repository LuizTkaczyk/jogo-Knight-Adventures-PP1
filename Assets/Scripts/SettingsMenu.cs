using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider sliderMain;
    public Slider sliderMusic;
    public Slider sliderEffects;


    public float sliderValueMain;
    public float sliderValueMusic;
    public float sliderValueEffects;

    public AudioMixer audioMixer;

    private void Start()
    {
        sliderMain.value = PlayerPrefs.GetFloat("main", sliderValueMain);
        sliderMusic.value = PlayerPrefs.GetFloat("music", sliderValueMusic);
        sliderEffects.value = PlayerPrefs.GetFloat("effect", sliderValueEffects);
    }


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

}
