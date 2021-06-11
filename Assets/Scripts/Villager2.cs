using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager2 : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rig;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rig = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Player.exeOneTime == 1)
        {
            Walk();
        }
        
    }


    public void Walk()
    {
        transform.Translate(Vector2.right *4* Time.deltaTime);
        
        anim.SetTrigger("walk");
        Destroy(gameObject, 5f);
    }
}
