using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDrop : MonoBehaviour
{
    //Plataformas que caem no cenário do chefe final

    private Animator anim;
    private BoxCollider2D coll;
    public float timeDrop;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        coll = gameObject.GetComponent<BoxCollider2D>();
    }

    IEnumerator DropPlatform()
    {
        yield return new WaitForSeconds(timeDrop);
        gameObject.AddComponent<Rigidbody2D>().mass = 20;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("fall");
            StartCoroutine(DropPlatform());
            Destroy(gameObject, 5f);
        }
    }
}
