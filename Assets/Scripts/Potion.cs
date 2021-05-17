using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int LifeValue;
    public GameObject animLife;
    private SpriteRenderer sprite;
    private CircleCollider2D colliders;
    

    private void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        colliders = gameObject.GetComponent<CircleCollider2D>();
    }

    IEnumerator takeLifes()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(animLife);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            takeLifes();
            animLife.GetComponent<Animator>().SetTrigger("takePotion");

            Audios.current.PlayMusic(Audios.current.potion);
            Controller.current.AddLife(LifeValue);
            sprite.enabled = false;
            colliders.enabled = false;
            Destroy(gameObject,3f);

            
        }
    }
}
