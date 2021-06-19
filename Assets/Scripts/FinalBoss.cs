using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBoss : MonoBehaviour
{

    
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    private Animator anim;

    public Transform player;
    private GameObject playerP;

    public bool isFlipped = false;

    public Animator SceneTransition;

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

    public void TakeDemage(int demage)
    {
        currentHealth -= demage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 90)
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

            if (currentHealth <= 0)
            {
                anim.SetTrigger("die");
                this.GetComponent<Rigidbody2D>().simulated = false;

                StartCoroutine(FinalScene());
                

            }


        }

        if (collision.gameObject.tag == "PlatformDown")
        {
           
            TakeDemage(10);
           
            anim.SetTrigger("hurt");

            if (currentHealth <= 0)
            {
                anim.SetTrigger("die");
                this.GetComponent<Rigidbody2D>().simulated = false;

                StartCoroutine(FinalScene());
                

            }


        }

        IEnumerator FinalScene()
        {
            Player.exeOneTime = 0;
            yield return new WaitForSeconds(2f);
            SceneTransition.SetTrigger("Start");
            StartCoroutine(end());
           
        }

        IEnumerator end()
        {
            yield return new WaitForSeconds(5f); 
            SceneManager.LoadScene(12);


        }
    }
}
