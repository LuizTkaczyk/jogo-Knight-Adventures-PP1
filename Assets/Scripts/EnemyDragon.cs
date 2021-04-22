using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{

    public float AtkInterval; //intervalo de ataque do dragão
    public float AtkDistance; //distancia em que  dragão vai atacar o player

    private float IntervalAtk;
    private bool isAtk;
    public GameObject FireBall; // bola de fogo lançada pelo dragão

    private Animator anim;//chama o animator
    private GameObject player; //chama o animator

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //chama o animator
        player = GameObject.FindGameObjectWithTag("Player"); // chama o player pela tag
    }

    // Update is called once per frame

    Vector3 firePos; // o fogo do dragão sai um pouco a fremte da boca

    void Update()
    {
        if (player != null)
        {

            float Distance = player.transform.position.x - transform.position.x; //distancia entre o player e o dragão em relação ao eixo x

            if (Distance > 0)
            {
                transform.eulerAngles = new Vector2(0, 0); //direção em que o dragão olha 
                firePos = new Vector3(0.5f, 0, 0); // o fogo do dragão sai um pouco a fremte da boca

            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
                firePos = new Vector3(-0.5f, 0, 0);
            }


            if (!isAtk && Mathf.Abs(Distance) <= AtkDistance) //executa a animação de ataque do dragão e joga a bola de fogo conforme a distancia indicada
            {
                Audios.current.PlayMusic(Audios.current.fireBall);
                anim.SetTrigger("atk");
                Instantiate(FireBall, transform.position + firePos, transform.rotation);
                isAtk = true;
            }

            if (isAtk) // o dragão para de atacar
            {
                IntervalAtk += Time.deltaTime;
                if (IntervalAtk >= AtkInterval)
                {
                    isAtk = false;
                    IntervalAtk = 0;
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
            

            anim.SetTrigger("die");
            Destroy(this);
            Destroy(gameObject, 0.7f);

        }
    }
}
