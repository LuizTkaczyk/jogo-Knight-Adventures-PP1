using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{

    Rigidbody2D rig;
    Vector2 moveDirection;
    //public float axeHeightThrown;


    public float Speed;
    //public float Demage;

    // Start is called before the first frame update
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);

        //moveDirection = (transform.position - transform.position).normalized;
        //rig.velocity = new Vector2(moveDirection.x, moveDirection.y + axeHeightThrown);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }

    

   
}
