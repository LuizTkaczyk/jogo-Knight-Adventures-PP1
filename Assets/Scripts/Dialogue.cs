using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3,10)]
    public string[] sentences;




















    //public float radious;
    //public LayerMask playerLayer;

    //public void Interact()
    //{
    //    Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);

    //    if (hit != null)
    //    {
    //    }
    //    else
    //    {

    //    }
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawWireSphere(transform.position, radious);
    //}
}
