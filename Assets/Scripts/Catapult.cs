using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour
{

    public GameObject catapultBall;
    public Transform shotPoint;

    //private GameObject bolaCriada;
    private GameObject player;
    private Animator anim;
    private Vector3 catapultBallPosition;
    private bool isAtk;
    //timer da bola
    //private float tpsLimit = 10f;
    //private float timerBallCatapult = 0;
    private float timerBall =0f ;
    public float DistanceAtk;
    private float IntervalAtk = 2.5f;
    private float DistanceInterval;


    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    IEnumerator AtaqueBolaCatapulta()
    {
        yield return new WaitForSeconds(timerBall);
        Shoot();

    }

    void Shoot()
    {
       
        //Instantiate(catapultBall, shotPoint.position, shotPoint.rotation);
        StartCoroutine(timerBallCat());
        anim.SetTrigger("AtaqueCata");
        //Audios.current.PlayMusic(Audios.current.arrow);

    }

    IEnumerator timerBallCat()
    {
        yield return new WaitForSeconds(0.50f);

        Instantiate(catapultBall, shotPoint.position, shotPoint.rotation);

    }



    void Update()
    {
        if (player != null)
        {
            float Distance = player.transform.position.x - transform.position.x;

            if (Distance > 0)
            {
                transform.eulerAngles = new Vector2(0, 0);
                catapultBallPosition = new Vector3(0.5f, -0.05f, 0); 
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
                catapultBallPosition = new Vector3(-0.5f, -0.05f, 0);

            }

            if (!isAtk && Mathf.Abs(Distance) <= DistanceAtk)
            {
               
                StartCoroutine(AtaqueBolaCatapulta());
                isAtk = true;
            }

            if (isAtk)
            {
                DistanceInterval += Time.deltaTime;
                if (DistanceInterval >= IntervalAtk)
                {
                    isAtk = false;
                    DistanceInterval = 0;
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
                //collision.gameObject.transform.Translate(-Vector2.right * 0.5f); // força do empurrão do inimigo
                Controller.current.RemoveLife(1); //perde uma vida

                StartCoroutine(collision.gameObject.GetComponent<Player>().PlayerDemage(0.05f));
                collision.gameObject.GetComponent<Player>().isVisible = true;
            }
        }


        if (collision.gameObject.layer == 9) //layer do machado !
        {
            anim.SetTrigger("Explosion");
            Audios.current.audioSurce.PlayOneShot(Audios.current.cataExplosion, 0.2f);//recebe o audio da explosão da catapulta e aplica o volume de 0.2 
            //GetComponent<BoxCollider2D>().enabled = false; //o colisor é desativado ao destruir o inimigo
            GetComponent<Rigidbody2D>().simulated = false;
            Destroy(gameObject, 1.5f);
            StopCoroutine("AtaqueBolaCatapulta");
            Destroy(this);
            

        }
    }
}