using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    public bool isPaused;
    public float Speed;
    public bool isJumping;
    public bool DoubleJump;
    public float JumpForce;
    public bool isAtack; //verifica se esta atacando
    private float timeAtk;
    public GameObject point; // esse é a referencia ao machado do player
    public SpriteRenderer SpriteRend;
    private float visibleTime = 1.4f; //tempo em que o player fica invisivel ao receber um dano
    public bool isVisible;
    private float visibleCount;
    public bool isAlive;

    //Machado arremessado
    public GameObject Axe;
    private Vector3 PosMachado;
    public GameObject AxePitch;

    //timer do machado 
    public float tpsLimite = 10f;
    private float timerAxe = 0;


    //posição inicial do player
    private Vector2 posInitial;

    private GameControllerCheck gcc;

    //Configurações do player, como andar para as devidas direções, animação de andar, de virar pra trás
    void Start()
    {
        //determina a posição no inicio do jogo
        posInitial = new Vector2(-12.89f, -1.37f);
        transform.position = posInitial;

        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SpriteRend = GetComponent<SpriteRenderer>();

    }

    IEnumerator AtaqueMachado()
    {
        yield return new WaitForSeconds(0.4f);
        Instantiate(Axe, AxePitch.transform.position, transform.rotation);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Movements();


    }

    private void Movements()
    {
        //COMANDOS PARA PC--------------------------------------------------------------------------------------------------------------------------------------------
        if (isAlive == true)
        {

            //controles Xbox
            if ((Input.GetAxisRaw("HorizontalJoystick") > 0) || Input.GetKey(KeyCode.D)) //vira/vai pra direita , com o teclado

            {
                rig.velocity = new Vector2(Speed * Time.deltaTime, rig.velocity.y); //faz o personagem andar para a direita, o time.delta time deixa o movimento mais suave
                anim.SetBool("isWalking", true); // ativa a animação de andar
                transform.localEulerAngles = new Vector3(0, 0, 0); //deixa o player virado para a direita ao pressionar para a direita
                PosMachado = new Vector3(0.500f, 0.680f, 0); //Posição do machado

            }

            else
            {
                rig.velocity = new Vector2(0f, rig.velocity.y); //codigo para o player não deslizar apos parar de correr
                anim.SetBool("isWalking", false); // ativa a animação de andar

            }


            // if (Input.GetKey(KeyCode.A))
            if ((Input.GetAxisRaw("HorizontalJoystick") < 0) || Input.GetKey(KeyCode.A))  //vira/vai pra esquerda

            {
                rig.velocity = new Vector2(-Speed * Time.deltaTime, rig.velocity.y); //deixa o speed negativo para ele se movimetar para a esquerda
                anim.SetBool("isWalking", true); // ativa a animação de andar
                transform.localEulerAngles = new Vector3(0, 180, 0); // deixa o player virado para a esquerda ao pressionar para a esquerda
                PosMachado = new Vector3(-0.500f, 0.680f, 0); //Posição do machado
            }

            // if (Input.GetKeyDown(KeyCode.Space))
            if (Input.GetButtonDown("JumpJoystick") || Input.GetKeyDown(KeyCode.Space))

            {

                if (!isJumping)
                {
                    rig.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse); // codigo para o pulo
                    isJumping = true;
                    anim.SetBool("isJump", true);
                    DoubleJump = true;
                    Audios.current.PlayMusic(Audios.current.jumpSfx);

                }
                else
                {
                    if (DoubleJump)//pulo duplo
                    {
                        rig.AddForce(Vector2.up * (JumpForce * 0.9f), ForceMode2D.Impulse); // codigo para o pulo
                        isJumping = true;
                        anim.SetBool("isJump", true);
                        DoubleJump = false;
                        Audios.current.PlayMusic(Audios.current.jumpSfx);
                    }
                }

            }


            if (timerAxe > 0)
            {
                timerAxe -= Time.deltaTime;
            }
            else if ((Input.GetButtonDown("AtackJoystick") || Input.GetKeyDown(KeyCode.K))) //codigo de ataque

            {
                timerAxe = 5f / tpsLimite;

                Audios.current.PlayMusic(Audios.current.atkSfx);
                anim.SetBool("isAtk", true);
                anim.SetBool("animMachado", true);
                timeAtk = 0.50f;

                StartCoroutine(AtaqueMachado());
                isAtack = true;

            }

            timeAtk -= Time.deltaTime; //ativado tanto nos comandos mobile quanto pc

            if (timeAtk <= 0f)
            {
                anim.SetBool("isAtk", false); // codigo de ataque
                isAtack = false;
                point.SetActive(false);

            }

            if (isVisible == true) //ativa quando recebe danos de um inimigo próximo, aquelas piscadinhas
            {

                visibleCount += Time.deltaTime;
                if (visibleCount >= visibleTime)
                {
                    //Debug.Log("acertou");
                    visibleCount = 0;
                    isVisible = false;
                    this.GetComponent<SpriteRenderer>().color = Color.white;

                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("isJump", false);
        }

        if (collision.gameObject.layer == 16)
        {
            isJumping = false;
            anim.SetBool("isJump", false);
        }


        if (collision.gameObject.tag == "checkpoint")
        {
            posInitial = collision.gameObject.transform.position; //pega a posição do player ao passar no checkpoint
            transform.position = posInitial;
            //Destroy(collision.gameObject);
        }

        if (collision.gameObject.layer == 10)

        {

            Controller.current.RemoveLife(1);
            gcc = GameObject.FindGameObjectWithTag("GC").GetComponent<GameControllerCheck>();
            transform.position = gcc.LastCheckpoint;


        }
    }

    public IEnumerator PlayerDemage(float DemageTime)
    {
        for (int i = 0; i < 15; i++)
        {
            SpriteRend.color = Color.red;
            yield return new WaitForSeconds(DemageTime); //depois n segundos é executado o comando listado abaixo

            SpriteRend.color = Color.white;
            yield return new WaitForSeconds(DemageTime);

        }

    }

}
