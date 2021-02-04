using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int LifeValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Audios.current.PlayMusic(Audios.current.potion);
            Controller.current.AddLife(LifeValue);
            Destroy(gameObject);
        }
    }
}
