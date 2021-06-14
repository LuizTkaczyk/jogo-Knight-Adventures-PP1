using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager1 : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rig;

    public static Villager1 current;

    private void Start()
    {
        current = this;
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
        transform.Translate(Vector2.left *3* Time.deltaTime);
        
        anim.SetTrigger("walk");
        Destroy(gameObject,10f);
    }

}
