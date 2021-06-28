using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    //Codigo do Checkpoint

    private GameControllerCheck GC;
    private Animator anim;
    public GameObject checkLetters;
    public GameObject platform;
    public GameObject platformPoint;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        GC = GameObject.FindGameObjectWithTag("GC").GetComponent<GameControllerCheck>(); //encontra o objeto na cena com a tag "GC"
        platform = GameObject.FindGameObjectWithTag("PlatformDown");
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
