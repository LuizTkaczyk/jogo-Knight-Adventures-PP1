using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    //Código do menu de pausa, comando como voltar o jogo, sair do jogo

    public static PauseMenu current;
    public GameObject pauseMenu;
    public GameObject RestartMenu;
    public GameObject pausePrimeiroBtn;
    public bool isAlive;
    public static bool inputEnable = true; // bool que verifica se os btns estão ativados ou não

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

    }

    // Update is called once per frame
    void Update()
    {

        if (inputEnable)
        {
            if ((Input.GetKeyDown(KeyCode.Escape)) || (Input.GetButtonDown("PauseJoystick")))
            {
                PauseUnpause();
            }
        }
    }

    public void PauseUnpause()
    {


        if (!pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void optionsMenuOff()
    {
        inputEnable = false;
    }
    public void optionsMenuOn()
    {
        inputEnable = true;
    }



    public void Restart()
    {
        Destroy(Controller.current);
        RestartMenu.SetActive(false);
        Controller.current.StartGame();
        Player.exeOneTime = 0;
    }

    public void MenuRestart()
    {
        RestartMenu.SetActive(true);

    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
        pauseMenu.SetActive(false);
        Player.exeOneTime = 0;

    }

}
