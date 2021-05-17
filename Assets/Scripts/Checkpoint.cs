using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameControllerCheck GC; //referencia a classe GameControllercheck
    private Animator anim;
    public GameObject checkLetters;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        
        GC = GameObject.FindGameObjectWithTag("GC").GetComponent<GameControllerCheck>(); //encontra o objeto na cena com a tag "GC"
    }

     IEnumerator timerCheckLetters()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(checkLetters);
        

    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // aqui o player recebe a posição do checkpoint
        {

            GC.LastCheckpoint = transform.position;
            anim.SetTrigger("checkPoint1");
            timerCheckLetters();
            checkLetters.GetComponent<Animator>().SetTrigger("checkLetters");

           

        }
    }

   

}
