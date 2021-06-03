using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cam : MonoBehaviour
{

    private GameObject player;
    public float Speed;
    public float positionCam;
    public float positionPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // esse comando referencia o player
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {

            if (player.transform.position.x > positionCam)
            {
                Vector3 newPos = new Vector3(player.transform.position.x + positionPlayer, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, newPos, Speed * Time.deltaTime);//lerp é um movimento suave
            }

        }
    }
}
