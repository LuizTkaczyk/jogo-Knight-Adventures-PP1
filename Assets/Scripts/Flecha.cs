using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    Rigidbody2D rig;

    //perseguir o player
    float moveSpeed = 15f;
    private Player alvo;
    Vector2 moveDirection;
    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();//codigo para o arqueiro perseguir o player
        alvo = GameObject.FindObjectOfType<Player>();
        moveDirection = (alvo.transform.position - transform.position).normalized * moveSpeed;
        rig.velocity = new Vector2(moveDirection.x, moveDirection.y);//ate aqui
        
    }

    private void Update()
    {
        float angle = Mathf.Atan2(rig.velocity.y, rig.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (!collision.gameObject.GetComponent<Player>().isVisible)
            {

                Controller.current.RemoveLife(1);
                StartCoroutine(collision.gameObject.GetComponent<Player>().PlayerDemage(0.1f));
                collision.gameObject.GetComponent<Player>().isVisible = true;


            }


        }

       

    }
}
