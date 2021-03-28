using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag =="Player")
        //{
        //    Audios.current.PlayMusic(Audios.current.coins);
        //    Controller.current.AddScore(5);
        //    Destroy(gameObject);
        //}
    }
}
