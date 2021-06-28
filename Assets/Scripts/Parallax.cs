using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //Código do fundo paralax

    private float lenght;
    private float StartPos;
    private Transform cam;
    public float ParallaxEfect;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x; //captura o tamanho do sprite no eixo x
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float RePos = cam.transform.position.x * (1 - ParallaxEfect);
        float Distance = cam.transform.position.x * ParallaxEfect;
        transform.position = new Vector3(StartPos + Distance, transform.position.y, transform.position.z);

        if(RePos > StartPos + lenght / 2) //divide por 2 para q o o fundo aparece antes do fim da câmera
        {
            StartPos += lenght;

        }else if (RePos < StartPos - lenght / 2) //divide por 2 para q o o fundo aparece antes do fim da câmera
        {
            StartPos -= lenght;
        }
    }
}
