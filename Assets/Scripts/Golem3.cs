using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem3 : MonoBehaviour
{
    //Código do golem 3

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

    // Update is called once per frame


    void Update() 
    {
        if (Direction)  
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        else
        {
            transform.eulerAngles = new Vector2(0, 180); 
        }

        transform.Translate(Vector2.right * Speed * Time.deltaTime); 

        TimeDirection += Time.deltaTime;

        if (TimeDirection >= DurationDirection) 
        {
            TimeDirection = 0;
            Direction = !Direction;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            anim.SetTrigger("atk3");
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
            anim.SetTrigger("die3");
            Destroy(gameObject, 2f);
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().simulated = false;
        }
    }
}
