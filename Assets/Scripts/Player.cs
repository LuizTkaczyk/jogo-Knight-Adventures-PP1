﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Flowchart = Fungus.Flowchart;


public class Player : MonoBehaviour
{
    //Códigos do player - Buk

    private Rigidbody2D rig;
    private Animator anim;
    public bool isPaused;
    public float Speed;
    public float JumpForce;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask isGround;
    private bool isGrounded;
    private int extraJumps;
    public int extraJumpValue;
    public bool isJumping;
    public bool DoubleJump;
    public bool isAtack; //verifica se esta atacando
    private float timeAtk;
    public GameObject point; 
    public SpriteRenderer SpriteRend;
    private float visibleTime = 1.4f;
    public bool isVisible;
    private float visibleCount;
    public bool isAlive;
    public bool movements;

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

    //Caixa de diálogo
    public Flowchart fungus;
    public static int exeOneTime = 0;
    private GameObject villager;
    private WaitForSeconds time;
    private bool villagerLive = true;
    public static bool inputEnable = true;
    public static bool inputMove = true;

    //Configurações do player, como andar para as devidas direções, animação de andar, de virar pra trás
    void Start()
    {

        extraJumps = extraJumpValue;
        //determina a posição no inicio do jogo
        posInitial = new Vector2(-14.89f, -1.37f);
        transform.position = posInitial;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SpriteRend = GetComponent<SpriteRenderer>();
        villager = GameObject.FindGameObjectWithTag("Villager");
        time = new WaitForSeconds(2.5f);
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

    private void Update()
    {
        if (villagerLive)
        {
            Dialog();
        }
    }

    void Dialog()
    {

        if (exeOneTime == 0)
        {
            if (Vector2.Distance(transform.position, villager.transform.position) < 2f)
            {
                villagerLive = true;
                PauseMenu.inputEnable = false;
                inputEnable = false;
                isJumping = true;
                Speed = 0;
                fungus.ExecuteBlock("Start");
                villager.GetComponent<Animator>().SetBool("talk", true);

            }

        }
    }



    public void CloseDialog()
    {
        PauseMenu.inputEnable = true;
        inputEnable = true;
        isJumping = false;
        exeOneTime = 1;
        Speed = 200;
        villager.GetComponent<Animator>().SetBool("talk", false);
        StartCoroutine(VillageDialog());
        villagerLive = false;

    }

    IEnumerator VillageDialog()
    {
        yield return new WaitForSeconds(5f);
        exeOneTime = 0;
    }


    private void Movements()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, isGround);

        if (isAlive == true)
        {

            //if (Input.GetKey(KeyCode.L)) 

            //{
            //    Controller.current.AddLife(1);

            //}



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


            if(isGrounded == true)
            {
                extraJumps = extraJumpValue;
            }

            // if (Input.GetKeyDown(KeyCode.Space))
            if (Input.GetButtonDown("JumpJoystick") && extraJumps > 0 || Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)

            {
                rig.velocity = Vector2.up * JumpForce;
                anim.SetBool("isJump", true);
                Audios.current.PlayMusic(Audios.current.jumpSfx);

                extraJumps--;
            }else if(Input.GetButtonDown("JumpJoystick") && extraJumps > 0 && isGrounded == true || Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
            {
                rig.velocity = Vector2.up * JumpForce;
                anim.SetBool("isJump", true);
                Audios.current.PlayMusic(Audios.current.jumpSfx);

            }


            if (inputEnable)
            {
                if (timerAxe > 0)
                {
                    timerAxe -= Time.deltaTime;
                }
                else if ((Input.GetButtonDown("AtackJoystick") || Input.GetKeyDown(KeyCode.K))) //codigo de ataque

                {
                    timerAxe = 5f / tpsLimite;

                    Audios.current.PlayMusic(Audios.current.atkSfx);
                    anim.SetBool("isAtk", true);
                    //anim.SetBool("animMachado", true);
                    timeAtk = 0.50f;

                    StartCoroutine(AtaqueMachado());
                    isAtack = true;

                }

                timeAtk -= Time.deltaTime; 

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
