using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow2 : MonoBehaviour
{

    //Código da flecha do arqueiro
    Rigidbody2D rig;
    public float moveSpeed;
    private GameObject target;
    Vector2 moveDirection;
    private Animator anim;
    public float elevationArrow;


    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();//codigos para o arqueiro perseguir o player
        target = GameObject.FindGameObjectWithTag("Player");


        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;

        if (target.GetComponent<Rigidbody2D>().velocity.x == 0) //pega a velocidade do player, caso maior que  0 o arqueiro acompanha e acerta, caso for 0 o arqueiro acerta do mesmo modo
        {

            rig.velocity = new Vector2(moveDirection.x, moveDirection.y + elevationArrow);//ate aqui

        }

        if (target.GetComponent<Rigidbody2D>().velocity.x < 0)
        {

            rig.velocity = new Vector2(moveDirection.x - 3, moveDirection.y + elevationArrow);//ate aqui
        }

        if (target.GetComponent<Rigidbody2D>().velocity.x > 0)
        {

            rig.velocity = new Vector2(moveDirection.x + 3, moveDirection.y + elevationArrow);//ate aqui
        }


        Destroy(gameObject, 3f);
        
    }

    private void Update()
    {
        //faz a flecha ter angulo de queda
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
