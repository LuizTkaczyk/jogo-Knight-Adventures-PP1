using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioMenu : MonoBehaviour
{
    //Audio do menu principal

    private static AudioMenu instanceAudio;

    // Start is called before the first frame update
    void Start()
    {
        musicMenu();
    }

    void musicMenu()
    {
        DontDestroyOnLoad(this);

        if (instanceAudio == null)
        {
            instanceAudio = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
