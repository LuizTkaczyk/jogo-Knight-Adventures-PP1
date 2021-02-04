using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AtkArrowUi : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Player player;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //chama o player
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        player.isAtk = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.isAtk = false;
    }

}
