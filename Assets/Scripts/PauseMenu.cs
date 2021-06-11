﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu current;
    public GameObject pauseMenu;
    public GameObject RestartMenu;
    public GameObject pausePrimeiroBtn;
    // public GameObject canvas;
    public bool isAlive;

    //teste desbilitar teclado
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
            if ((Input.GetKeyDown(KeyCode.Escape)) || (Input.GetButtonDown("PauseJoystick")))  //ESc OU start do controle
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
    }

    public void MenuRestart()
    {
        RestartMenu.SetActive(true);

    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
        pauseMenu.SetActive(false);

    }

}