using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public static Controller current;

    public GameObject life; //prefab instanciado
    private int PlayerLives;
    private Transform Lives;

    private GameObject GameOverPanel; //chama o canvas do game over

    private bool isPaused;

    private Animator anim;
    private GameObject player;
    private GameObject golom;
    private GameObject dragon;
    private GameObject menu;
    private GameObject bgm;


    // Start is called before the first frame update
    void Start()
    {


        current = this;
        bgm = GameObject.FindGameObjectWithTag("bgmMenu");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player"); //faz referencia ao player na classe controller
        golom = GameObject.FindGameObjectWithTag("Golom"); //faz referencia ao Golom na classe controller
        dragon = GameObject.FindGameObjectWithTag("Dragon"); //faz referencia ao Dragon na classe controller
        menu = GameObject.FindGameObjectWithTag("MenuPause");

        GameOverPanel = GameObject.FindGameObjectWithTag("MenuPause");

        if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "Config")
        {
            Lives = GameObject.FindWithTag("Lives").transform;
        }

    }

    public void AddLife(int lifeValue) //add vidas ao personagem
    {
        if (PlayerLives < 4)
        {
            PlayerLives += lifeValue;

            for (int i = 0; i < lifeValue; i++)
            {
                Instantiate(life, Lives.transform);
            }
        }

    }


    public void RemoveLife(int lifeValue)

    {
        if (Lives.childCount > 0)
        {
            PlayerLives -= lifeValue;

            for (int i = 0; i < lifeValue; i++)
            {
                Destroy(Lives.GetChild(i).gameObject);

            }
        }

        if (Lives.childCount < 1)
        {
            menu.GetComponent<PauseMenu>().enabled = false; // desativa o menu de pausa ao morrer
            player.GetComponent<Player>().isAlive = false;
            player.GetComponent<Animator>().SetBool("playerDie", true);//pega a animação de morte da classe do player
            player.GetComponent<Player>().Speed = 0;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX; // congela o player onde estiver
            player.GetComponent<Rigidbody2D>().simulated = false; // ativa a opção simulated, que deixa o player sem massa.
            GameOverPanel.GetComponent<PauseMenu>().MenuRestart();

        }
    }


    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //recarrega o cenario atual depois de morrer

    }

    public void StartGame()
    {
        Time.timeScale = Time.timeScale = 1;

        SceneManager.LoadScene(6);
        Destroy(bgm);
        Destroy(menu);
    }

    public void Options()
    {
        SceneManager.LoadScene(11);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }




    public void Quit()
    {
        SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);
        }
        else
        {
            Application.Quit();
        }
    }

}
