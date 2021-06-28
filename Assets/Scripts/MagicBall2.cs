using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall2 : MonoBehaviour
{
    //Bola de magia do fantasma

    public float Speed;
    public float Damage;
    private Animator anim;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4f);
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("magicBall");
            anim.GetComponent<CircleCollider2D>().enabled = false;
            player.GetComponent<Animator>().SetTrigger("Dano");
            Speed = 1f;

            if (!collision.gameObject.GetComponent<Player>().isVisible)
            {
                Controller.current.RemoveLife(1);
                StartCoroutine(collision.gameObject.GetComponent<Player>().PlayerDemage(0.05f));
                collision.gameObject.GetComponent<Player>().isVisible = true;
            }


        }
    }
}
