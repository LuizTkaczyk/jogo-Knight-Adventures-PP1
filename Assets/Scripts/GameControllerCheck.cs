using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerCheck : MonoBehaviour
{
    //Código para saber a posição do checkpoint
   
    public static GameControllerCheck instance;
    public Vector2 LastCheckpoint;

    void Start()
    {
        LastCheckpoint = new Vector2(-12.89f, -1.37f);
        
        if (instance == null)
        {
            instance = this;   
        }
        else
        {
            Destroy(gameObject);
           
        }

    }


}
