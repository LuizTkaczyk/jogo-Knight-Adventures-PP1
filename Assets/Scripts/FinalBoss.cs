using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    private Animator anim;

    private void Start()
    {

        //barra de vida do boss
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        anim = gameObject.GetComponent<Animator>();
    }

    //retira vida do boss
    void TakeDemage(int demage)
    {
        currentHealth -= demage;
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player") // se o inimigo bater no player , o inimigo ataca
        //{


        //        collision.gameObject.transform.Translate(-Vector2.right * 0.5f); // força do empurrão do inimigo
        //        Controller.current.RemoveLife(1); //perde uma vida
        //        StartCoroutine(collision.gameObject.GetComponent<Player>().PlayerDemage(0.05f));
        //        collision.gameObject.GetComponent<Player>().isVisible = true;

        //}

        if (collision.gameObject.layer == 9) //layer do machado !
        {

            anim.SetTrigger("hurt");
            TakeDemage(10);
            


        }
    }

   


}
