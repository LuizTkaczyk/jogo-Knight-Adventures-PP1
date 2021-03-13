using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machado : MonoBehaviour
{
    

    public float Speed;
    //public float Demage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }

    

   
}
