using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    public GameObject life;
    public int PlayerLives;
    public int Score;
    public Text ScoreText;

    public Transform Lives;

    public GameObject GameOverPanel; //chama o canvas do game over
    //public GameObject btnPause; // referencia ao botão de pause


    public static Controller current;

   
    public bool isPaused;



    private Animator anim;
    private GameObject player;
    private GameObject golom;
    private GameObject dragon;
    private GameObject menu;


    // Start is called before the first frame update
    void Start()
    {
        //AddLife(3);
        //RemoveLife(2);
        current = this;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player"); //faz referencia ao player na classe controller
        golom = GameObject.FindGameObjectWithTag("Golom"); //faz referencia ao Golom na classe controller
        dragon = GameObject.FindGameObjectWithTag("Dragon"); //faz referencia ao Dragon na classe controller
        menu = GameObject.FindGameObjectWithTag("MenuPause");

    }

    // Update is called once per frame
    void Update()
    {
        if(ScoreText != null) { // retira o erro da tela de new game

         ScoreText.text = Score.ToString(); // ToString() converte o numero pra string
        }

    }

    public void AddLife(int lifeValue) //add vidas ao personagem
    {


        if (PlayerLives < 4) { 

            PlayerLives += lifeValue;

            for (int i = 0; i < lifeValue; i++ )
            {
                Instantiate(life, Lives.transform);
            }
        }

        
        
    }

    public void RemoveLife(int lifeValue) //remove as vidas do player
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

                menu.GetComponent<MenuPause>().enabled = false; // desativa o menu de pausa ao morrer

                //current.pauseMenu.SetActive(false); //desativa o botão de pause ao morrer
               
                player.GetComponent<Player>().isAlive = false;
                player.GetComponent<Animator>().SetBool("playerDie", true);//pega a animação de morte da classe do player
                player.GetComponent<Player>().Speed = 0;
               
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX; // congela o player onde estiver
                player.GetComponent<Rigidbody2D>().simulated = false; // ativa a opção simulated, que deixa o player sem massa.
                

                //golom.GetComponent<Animator>().SetBool("atk1", false);
                //dragon.GetComponent<Animator>().SetBool("atk", false);


                current.GameOverPanel.SetActive(true); //chama a tela de game-over

 
            }


        
    }


    public void GameOver() {

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //recarrega o cenario atual depois de morrer
        

    }

    public void StartGame()
    {


        Time.timeScale = Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }



    //public void Pause() //comando para pausar e des-pausar o jogo
    //{
    //    isPaused = !isPaused;
    //    pauseMenu.SetActive(isPaused);
    //    Time.timeScale = Time.timeScale == 0 ? 1 : 0; //Esse comando Pausa o tempo do jogo

    //}




    public void AddScore(int ScoreValue)
    {
        Score += ScoreValue;
    }

    public void QuitGame()
    {
        if(Application.platform == RuntimePlatform.Android)
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
