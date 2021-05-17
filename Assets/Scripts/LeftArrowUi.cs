using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftArrowUi : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Player player;
   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //chama o player
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        player.isLeft = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.isLeft = false;
    }
    
}
