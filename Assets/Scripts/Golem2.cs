using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem2 : MonoBehaviour
{
    public float Speed; //define a velocidade do inimigo

    public bool Direction;//define as direções do inimigo
    public float DurationDirection;// define o tempo em que o inimigo andara em cada direção


    private Animator anim;
    private float TimeDirection;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame


    void Update() // cofigura se o inimigo vai para a esquerda ou direita
    {
        if (Direction)  // rotação do inimigo
        {
            transform.eulerAngles = new Vector2(0, 0); //olhando para a direita
        }

        else
        {
            transform.eulerAngles = new Vector2(0, 180); //olhando para a esquerda
        }

        transform.Translate(Vector2.right * Speed * Time.deltaTime); //movimenta o inimigo

        TimeDirection += Time.deltaTime;

        if (TimeDirection >= DurationDirection) //inverte o boleano direction de acordo com o tempo
        {
            TimeDirection = 0;
            Direction = !Direction;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // se o inimigo bater no player , o inimigo ataca
        {
            anim.SetTrigger("atk2");


            if (!collision.gameObject.GetComponent<Player>().isVisible)
            {
                collision.gameObject.transform.Translate(-Vector2.right * 0.5f); // força do empurrão do inimigo
                Controller.current.RemoveLife(1); //perde uma vida
                StartCoroutine(collision.gameObject.GetComponent<Player>().PlayerDemage(0.05f));
                collision.gameObject.GetComponent<Player>().isVisible = true;
            }
        }

        if (collision.gameObject.layer == 9) //layer do machado !
        {
            Speed = 0;
            anim.SetTrigger("die2");
            Destroy(gameObject,2f);

            GetComponent<CircleCollider2D>().enabled = false; //o colisor é desativado ao destruir o inimigo
            GetComponent<Rigidbody2D>().simulated = false;


        }
    }
}
