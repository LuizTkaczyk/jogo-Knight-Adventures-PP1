using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audios : MonoBehaviour
{

    public static Audios current;
    public AudioClip jumpSfx;
    public AudioClip atkSfx;
    public AudioClip potion;
    public AudioClip coins;
    public AudioClip fireBall;
    public AudioSource audioSurce;
    public AudioClip arrow;
    public AudioClip deathArcher;
    public AudioClip cataExplosion;




    // Start is called before the first frame update
    void Start()
    {
        
        current = this;

        audioSurce = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip audios)
    {
        audioSurce.PlayOneShot(audios);
    }

    
}
