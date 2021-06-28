using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    //Código do machado do player

    Rigidbody2D rig;
    Vector2 moveDirection;
    public float Speed;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);

    }

    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }

}
