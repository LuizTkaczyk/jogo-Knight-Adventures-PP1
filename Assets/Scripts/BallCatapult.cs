﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCatapult : MonoBehaviour
{
    //Código da bola da catapulta

    Rigidbody2D rig;
    public float moveSpeed = 15f;
    private Player target;
    Vector2 moveDirection;
    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Player>();
        
        //codigos perseguir o player
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rig.velocity = new Vector2(moveDirection.x, moveDirection.y + 4);
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
            anim.SetTrigger("bolaExplo");

            if (!collision.gameObject.GetComponent<Player>().isVisible)
            {
                collision.gameObject.transform.Translate(-Vector2.right * 0.5f);
                Controller.current.RemoveLife(2);

                StartCoroutine(collision.gameObject.GetComponent<Player>().PlayerDemage(0.040f));
                collision.gameObject.GetComponent<Player>().isVisible = true;

            }

        }

        if (collision.gameObject.layer == 8)
        {
            rig.simulated = false;
            anim.SetTrigger("bolaExplo");
            Destroy(gameObject, 0.25f);
        }

    }
}
