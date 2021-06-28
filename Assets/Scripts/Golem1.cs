using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem1 : MonoBehaviour
{
    //Código do golem 1

    public float Speed; 
    public bool Direction;
    public float DurationDirection;
    private Animator anim;
    private float TimeDirection;
   
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update() 
    {
        movimentoGolem();
    }

    void movimentoGolem()
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
        if (collision.gameObject.tag == "Player") 
        {
            anim.SetTrigger("atk1");
            if (!collision.gameObject.GetComponent<Player>().isVisible)
            {
                collision.gameObject.transform.Translate(-Vector2.right * 0.5f);
                Controller.current.RemoveLife(1);
                StartCoroutine(collision.gameObject.GetComponent<Player>().PlayerDemage(0.05f));
                collision.gameObject.GetComponent<Player>().isVisible = true;
            }
        }

        if (collision.gameObject.layer == 9)
        {
            Speed = 0;
            anim.SetTrigger("die");
            Destroy(gameObject, 2f);
            GetComponent<Rigidbody2D>().simulated = false;
        }
    }

}
