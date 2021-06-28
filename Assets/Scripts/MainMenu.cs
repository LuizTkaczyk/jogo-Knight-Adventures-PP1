using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    //Menu principal

    public GameObject MenuPrincip;
    public GameObject pauseFirstBtn;

    public void PrincipalMenu()
    {
        if (MenuPrincip.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(null); // limpando o botão selecionado pela unity por padrão
            EventSystem.current.SetSelectedGameObject(pauseFirstBtn);

        }
    }
        
}
