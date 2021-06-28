using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPoint : MonoBehaviour
{
    //Código que tranfere o player ao próximo cenário

    public string levelName;
    public Animator SceneTransition;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PauseMenu.inputEnable = false;
            StartCoroutine("LoadLevel");
            StartCoroutine("Invencible");
        }
    }

    public IEnumerator LoadLevel()
    {
        SceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(levelName);
    }

    public IEnumerator Invencible() //o inimigo não é atingido apos tocar o loadLevel
    {
        player.GetComponent<Rigidbody2D>().simulated = false;
        yield return new WaitForSeconds(3);

    }
}
