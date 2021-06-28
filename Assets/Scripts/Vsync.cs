using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vsync : MonoBehaviour
{
    // Sincronia vertical travada a 60 fps


    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
    }

}
