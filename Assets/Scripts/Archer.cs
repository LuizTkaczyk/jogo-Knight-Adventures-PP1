﻿using System.Collections;
using UnityEngine;

public class Archer : MonoBehaviour
{

    //Código do arqueiro
    public GameObject arrow;
    public Transform shotPoint;
    private GameObject player;
    private Animator anim;
    private Vector3 posFlecha;
    private bool isAtk;
    public float DistanciaAtaque;
    public float IntervaloAtaque;
    private float IntervaloDistancia;

    void Start()
    {
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");

    }

    IEnumerator AtaqueFlecha()
    {
        yield return new WaitForSeconds(IntervaloAtaque);
        
        Shoot();

    }

    void Shoot()
    {
        StartCoroutine(timerArrow());
        StartCoroutine(soundTimer());
        anim.SetTrigger("Ataque");
       
    }


    

    IEnumerator timerArrow()
    {
        yield return new WaitForSeconds(0.69f);
       
        Instantiate(arrow, shotPoint.position, shotPoint.rotation);

    }

    IEnumerator soundTimer()
    {
        yield return new WaitForSeconds(0.40f);
        Audios.current.PlayMusic(Audios.current.arrow);
    }

    private void Update()
    {
        if (player != null)
        {
            float Distance = player.transform.position.x - transform.position.x;

            if (Distance > 0)
            {
                transform.eulerAngles = new Vector2(0, 0); // direção que o arqueiro olha
                posFlecha = new Vector3(0.5f, -0.05f, 0); //posição que a flecha sai
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
                posFlecha = new Vector3(-0.5f, -0.05f, 0);

            }

            if (!isAtk && Mathf.Abs(Distance) <= DistanciaAtaque)
            {
                
                StartCoroutine(AtaqueFlecha());
                isAtk = true;
            }

            if (isAtk)
            {
                IntervaloDistancia += Time.deltaTime;
                if (IntervaloDistancia >= IntervaloAtaque)
                {
                    isAtk = false;
                    IntervaloDistancia = 0;
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player") // se o inimigo bater no player , o inimigo ataca
        {
            if (!collision.gameObject.GetComponent<Player>().isVisible)
            {
                
                Controller.current.RemoveLife(1); //perde uma vida
                StartCoroutine(collision.gameObject.GetComponent<Player>().PlayerDemage(0.05f));
                collision.gameObject.GetComponent<Player>().isVisible = true;
            }
        }


        if (collision.gameObject.layer == 9) //layer do machado !
        {
            GetComponent<CircleCollider2D>().enabled = false; //o colisor é desativado ao destruir o inimigo
            Audios.current.PlayMusic(Audios.current.deathArcher);
            anim.SetTrigger("Die");
            Destroy(gameObject, 3f);
            Destroy(this);
        }
    }

}
