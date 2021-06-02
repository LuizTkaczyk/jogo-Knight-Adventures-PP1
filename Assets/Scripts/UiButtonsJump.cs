using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButtonsJump : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }


    IEnumerator timerUi()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("outJump");
       
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(timerUi());
          
        }
    }

    
}
