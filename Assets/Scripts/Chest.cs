using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int Coins;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pointAtk")
        {
            anim.SetBool("openBox",true);
            Controller.current.AddScore(Coins);
            //Destroy(gameObject);
        }
    }
}
