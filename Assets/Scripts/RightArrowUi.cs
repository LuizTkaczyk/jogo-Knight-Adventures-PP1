using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightArrowUi : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Player player;
   

    //comando para o mobile

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //chama o player
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        player.isRight = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.isRight = false;
    }
    
}
