using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapulta : MonoBehaviour
{

    public GameObject bolaCatapulta;
    public Transform shotPoint;

    private GameObject bolaCriada;
    private GameObject player;
    private Animator anim;
    private Vector3 posBolaCata;
    private bool isAtk;
    //timer da bola
    public float tpsLimite = 10f;
    public float timerBolaCata = 0;
    public float tempoBola;
    public float DistanciaAtaque;
    public float IntervaloAtaque;
    private float IntervaloDistancia;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Shoot()
    {
       
        Instantiate(bolaCatapulta, shotPoint.position, shotPoint.rotation);
        
        Audios.current.PlayMusic(Audios.current.flecha);

       

    }


    IEnumerator AtaqueBolaCatapulta()
    {
        yield return new WaitForSeconds(tempoBola);
        Shoot();

    }
    void Update()
    {
        if (player != null)
        {
            float Distance = player.transform.position.x - transform.position.x;

            if (Distance > 0)
            {
                transform.eulerAngles = new Vector2(0, 0); // direção que o arqueiro olha
                posBolaCata = new Vector3(0.5f, -0.05f, 0); //posição que a flecha sai
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
                posBolaCata = new Vector3(-0.5f, -0.05f, 0);

            }

            if (!isAtk && Mathf.Abs(Distance) <= DistanciaAtaque)
            {
                timerBolaCata =+ (timerBolaCata / tpsLimite);
                StartCoroutine(AtaqueBolaCatapulta());
                anim.SetTrigger("AtaqueCata");


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
                //collision.gameObject.transform.Translate(-Vector2.right * 0.5f); // força do empurrão do inimigo
                Controller.current.RemoveLife(1); //perde uma vida

                StartCoroutine(collision.gameObject.GetComponent<Player>().PlayerDemage(0.05f));
                collision.gameObject.GetComponent<Player>().isVisible = true;
            }
        }


        if (collision.gameObject.layer == 9) //layer do machado !
        {

            GetComponent<CircleCollider2D>().enabled = false; //o colisor é desativado ao destruir o inimigo
           
            Destroy(gameObject, 3f);
            Destroy(this);


        }
    }
}