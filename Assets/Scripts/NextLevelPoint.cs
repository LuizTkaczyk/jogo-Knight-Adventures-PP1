using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPoint : MonoBehaviour
{
    //public static NextLevelPoint current;
    public string levelName;

    public Animator transicaoCena;
    void Start()
    {
        
    }


    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
             StartCoroutine("LoadLevel");
        }
    }

    public IEnumerator LoadLevel()
    {
        transicaoCena.SetTrigger("Start");
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(levelName);
    }
}
