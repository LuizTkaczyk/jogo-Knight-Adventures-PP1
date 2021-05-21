using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll1 : MonoBehaviour
{
    public float speed;
    public float distance;
    public bool isRight = true;
    public Transform groundCheck;

    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        //PlayerDetect();
        EnemyMovement();

    }

    void EnemyMovement()
    {
        anim.SetTrigger("walk");

        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);

        if (ground.collider == false)
        {
            if (isRight == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isRight = false;

            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isRight = true;

            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // se o inimigo bater no player , o inimigo ataca
        {
            anim.SetTrigger("atack");


            if (!collision.gameObject.GetComponent<Player>().isVisible)
            {
                collision.gameObject.transform.Translate(-Vector2.right * 0.5f); // força do empurrão do inimigo
                Controller.current.RemoveLife(1); //perde uma vida
                StartCoroutine(collision.gameObject.GetComponent<Player>().PlayerDemage(0.05f));
                collision.gameObject.GetComponent<Player>().isVisible = true;
            }
        }

        if (collision.gameObject.layer == 9) //layer do machado 
        {


            speed = 0;
            anim.SetTrigger("die");
            Destroy(gameObject, 2f);
            //GetComponent<CircleCollider2D>().enabled = false; //o colisor é desativado ao destruir o inimigo
            GetComponent<Rigidbody2D>().simulated = false;


        }
    }
}
