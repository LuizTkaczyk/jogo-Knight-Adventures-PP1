using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameControllerCheck GC; //referencia a classe GameControllercheck
    

    void Start()
    {
        
        GC = GameObject.FindGameObjectWithTag("GC").GetComponent<GameControllerCheck>(); //encontra o objeto na cena com a tag "GC"
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // aqui o player recebe a posição do checkpoint
        {

            GC.ultimoCheckpoint = transform.position;

           

        }
    }

}
