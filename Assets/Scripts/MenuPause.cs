using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public static MenuPause current;
    public GameObject pauseMenu;
    public GameObject pausePrimeiroBtn;
    
    public GameObject BtnRestart;

    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    
    // Update is called once per frame
    void Update()
    {
           if ((Input.GetKeyDown(KeyCode.Escape)) || (Input.GetButtonDown("PauseJoystick")))
       
            {

              PauseUnpause();

           }
        
        
      

    }

    public void PauseUnpause()
    {
        if (!pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;

            EventSystem.current.SetSelectedGameObject(null); // limpando o botão selecionado pela unity por padrão
            EventSystem.current.SetSelectedGameObject(pausePrimeiroBtn);

            
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            
        }
    }

    public void Restart()
    {


        
        //EventSystem.current.SetSelectedGameObject(MenuReiniciar);
        Controller.current.StartGame();
       
        
    }
}
