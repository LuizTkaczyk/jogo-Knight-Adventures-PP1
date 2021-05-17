using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerCheck : MonoBehaviour
{
    //scrip para saber a posição do checkpoint
   
    public static GameControllerCheck instance;
    public Vector2 LastCheckpoint;
    

    // Start is called before the first frame update


    void Start()
    {
        LastCheckpoint = new Vector2(-12.89f, -1.37f);
        
        if (instance == null)
        {
            instance = this;
            //Destroy(instance);
            
        }
        else
        {
            Destroy(gameObject);
           
        }

    }


}
