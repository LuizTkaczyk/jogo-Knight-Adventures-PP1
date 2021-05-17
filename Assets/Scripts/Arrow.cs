using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rig;

    //perseguir o player
    float moveSpeed = 18f;
    private Player target;
    Vector2 moveDirection;
    private Animator anim;
    public float elevationArrow;


    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();//codigos para o arqueiro perseguir o player
        target = GameObject.FindObjectOfType<Player>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rig.velocity = new Vector2(moveDirection.x +5, moveDirection.y + elevationArrow);//ate aqui
        Destroy(gameObject, 3f);
        
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
                
                StartCoroutine(collision.gameObject.GetComponent<Player>().PlayerDemage(0.040f));
                collision.gameObject.GetComponent<Player>().isVisible = true;


            }


        }

       

    }
}
