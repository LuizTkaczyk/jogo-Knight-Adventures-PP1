using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    private Animator anim;

    public Transform player;
    private GameObject playerP;

    public bool isFlipped = false;

    private void Start()
    {

        //barra de vida do boss
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        anim = gameObject.GetComponent<Animator>();
        playerP = GameObject.FindGameObjectWithTag("Player");
    }


    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    //retira vida do boss
    public void TakeDemage(int demage)
    {
        currentHealth -= demage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 50)
        {
            anim.SetBool("run", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // se o inimigo bater no player , o inimigo ataca
        {

            if (!collision.gameObject.GetComponent<Player>().isVisible)
            {

                //Debug.Log("encostou");

                collision.gameObject.transform.Translate(-Vector2.right * 0.5f); // força do empurrão do inimigo

                Controller.current.RemoveLife(1); //perde uma vida
                StartCoroutine(collision.gameObject.GetComponent<Player>().PlayerDemage(0.05f));
                collision.gameObject.GetComponent<Player>().isVisible = true;
            }


        }

        if (collision.gameObject.layer == 9) //layer do machado !
        {

            anim.SetTrigger("hurt");
            TakeDemage(10);

            if (currentHealth == 0)
            {
                anim.SetTrigger("die");
                this.GetComponent<Rigidbody2D>().simulated = false;
                //Destroy(gameObject, 5f);
            }


        }

        if (collision.gameObject.tag == "PlatformDown")
        {
            Debug.Log("cabeça");
            anim.SetTrigger("hurt");
            TakeDemage(10);

        }



    }


}
