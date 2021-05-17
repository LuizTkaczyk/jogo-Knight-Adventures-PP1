using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject MenuPrincip;
    public GameObject pauseFirstBtn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrincipalMenu()
    {
        if (MenuPrincip.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(null); // limpando o botão selecionado pela unity por padrão
            EventSystem.current.SetSelectedGameObject(pauseFirstBtn);

        }
    }
        
}
