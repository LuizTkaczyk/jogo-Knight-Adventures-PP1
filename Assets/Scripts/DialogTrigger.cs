using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{ 
    public float radious;
    public LayerMask playerLayer;
    public Dialogue dialog;

    public void TriggerDialog()
    {
        FindObjectOfType<DialogControl>().StartDialogue(dialog);

    }

    //private void FixedUpdate()
    //{
    //    ColiisionText();
    //}

    //void ColiisionText()
    //{
    //    Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);

    //    if (hit != null)
    //    { 
    //        TriggerDialog();
    //    }
    //}



    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawWireSphere(transform.position, radious);
    //}
}
