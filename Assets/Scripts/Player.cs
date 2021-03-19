using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;

    
    public GameObject pauseMenu;
    public bool isPaused;

    public float Speed;

    public bool isJumping;
    public bool DoubleJump;
    public float JumpForce;


    public bool isAtack; //verifica se esta atacando

    private float timeAtk;

    public GameObject point; // esse é a referencia ao machado do player

    public SpriteRenderer SpriteRend;

    public float visibleTime; //tempo em que o player fica invisivel ao receber um dano
    public bool isVisible;
    private float visibleCount;

    public bool isAlive;

    //controles mobile
    public bool isRight;
    public bool isLeft;
    public bool isAtk;

    //Machado arremessado
    public GameObject Machado;
    Vector3 PosMachado;

    //timer do machado 
    public float tpsLimite = 10f;
    private float timerMachado = 0;


    //posição inicial do player
    public Vector2 posInicial;

    //respaw  player
    //public Checkpoint spawnCheck;
    private GameControllerCheck gcc;


    //Configurações do player, como andar para as devidas direções, animação de andar, de virar pra trás
    void Start()
    {
      

        //determina a posição no inicio do jogo
        posInicial = new Vector2(-12.89f, -1.37f);
        transform.position = posInicial;

        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SpriteRend = GetComponent<SpriteRenderer>();

        

    }


    IEnumerator AtaqueMachado()
    {
        yield return new WaitForSeconds(0.4f);
        Instantiate(Machado, transform.position + PosMachado, transform.rotation);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Walk(direcao);


        //COMANDOS PARA PC--------------------------------------------------------------------------------------------------------------------------------------------
        if (isAlive)
        {



            // if (Input.GetKey(KeyCode.D))
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

            
            if(timerMachado > 0)
            {
                timerMachado -= Time.deltaTime;
            }else if ((Input.GetButtonDown("AtackJoystick") || Input.GetKeyDown(KeyCode.K))) //codigo de ataque

            {
                timerMachado = 6f / tpsLimite;
                
                Audios.current.PlayMusic(Audios.current.atkSfx);
                anim.SetBool("isAtk", true);
                anim.SetBool("animMachado", true);
                timeAtk = 0.50f;
                
                StartCoroutine(AtaqueMachado());
                isAtack = true;
              
            }





            //if ((Input.GetKeyDown(KeyCode.Escape)) || (Input.GetButtonDown("PauseJoystick")))
            //{
               
            //        Controller.current.Pause();

            //}
           

            //COMANDOS PARA MOBILE-------------------------------------------------------------------------------------------------------------------------------- -

            //if (isRight) //vira/vai pra direita , com o teclado

            //{
            //    rig.velocity = new Vector2(Speed * Time.deltaTime, rig.velocity.y); //faz o personagem andar para a direita, o time.delta time deixa o movimento mais suave
            //    anim.SetBool("isWalking", true); // ativa a animação de andar
            //    transform.localEulerAngles = new Vector3(0, 0, 0); //deixa o player virado para a direita ao pressionar para a direita
            //}
            //else
            //{
            //    rig.velocity = new Vector2(0f, rig.velocity.y); //codigo para o player não deslizar apos parar de correr
            //    anim.SetBool("isWalking", false); // ativa a animação de andar

            //}



            //if (isLeft)  //vira/vai pra esquerda

            //{
            //    rig.velocity = new Vector2(-Speed * Time.deltaTime, rig.velocity.y); //deixa o speed negativo para ele se movimetar para a esquerda
            //    anim.SetBool("isWalking", true); // ativa a animação de andar
            //    transform.localEulerAngles = new Vector3(0, 180, 0); // deixa o player virado para a esquerda ao pressionar para a esquerda
            //}

            ////if (isJump && !isJumping) - não ligar, já tem um método

            ////{
            ////    rig.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse); // codigo para o pulo
            ////    isJumping = true;
            ////    anim.SetBool("isJump", true);
            ////    Audios.current.PlayMusic(Audios.current.jumpSfx);
            ////}

            //if (isAtk && !isAtack) //codigo de ataque

            //{
            //    Audios.current.PlayMusic(Audios.current.atkSfx);
            //    anim.SetBool("isAtk", true);
            //    isAtack = true;
            //    timeAtk = 0.7f;

            //    point.SetActive(true); // ponto de ataque do machado



            //}

            timeAtk -= Time.deltaTime; //ativado tanto nos comandos mobile quanto pc

            if (timeAtk <= 0f)
            {
                anim.SetBool("isAtk", false); // codigo de ataque
                isAtack = false;
                point.SetActive(false);

            }





            if (isVisible) //ativa quando recebe danos de um inimigo, aquelas piscadinhas
            {
                visibleCount += Time.deltaTime;
                if (visibleCount >= visibleTime)
                {
                    isVisible = false;
                    visibleCount = 0;
                }
            }

            //if (Input.GetKeyDown(KeyCode.Escape))
            //{
            //    isPaused = !isPaused;
            //    pauseMenu.SetActive(isPaused);
            //    Time.timeScale = Time.timeScale == 0 ? 1 : 0; //Esse comando Pausa o tempo do jogo
            //}





        }

    }

    



    //testes inputSystem

    //private void Walk(Vector2 dir)
    //{
    //    xVelocity = dir.normalized.x * Speed;
    //    rig.velocity = new Vector2(xVelocity, rig.velocity.y);
    //}

    //public void Movimento(InputAction.CallbackContext context)
    //{
    //    inputX = context.ReadValue<Vector2>().x;
    //    anim.SetBool("isWalking", true); // ativa a animação de andar
    //    transform.localEulerAngles = new Vector3(0, 0, 0); //deixa o player virado para a direita ao pressionar para a direita
    //    PosMachado = new Vector3(0.500f, 0.680f, 0); //Posição do machado
    //}






    //PULO NO MOBILE
    public void JumpBt()
    {
        if (isAlive)
        {
            if (!isJumping)
            {
                rig.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                isJumping = true;
                anim.SetBool("isJump", true);
                Audios.current.PlayMusic(Audios.current.jumpSfx);


            }
        }
    }

    //MACHADO NO MOBILE - não funciona

    //public void Atack()
    //{
    //    if (isAtack)
    //    {

            
    //            anim.SetBool("isAtk", true);
    //            isAtack = true;
    //            timeAtk = 0.25f;
    //            point.SetActive(true); // ponto de ataque do machado

    //    }

    //    timeAtk -= Time.deltaTime;

    //    if (timeAtk <= 0f)
    //    {
    //        anim.SetBool("isAtk", false); // codigo de ataque
    //        isAtack = false;
    //        point.SetActive(false);
    //    }





    //    if (isVisible) //ativa quando recebe danos de um inimigo, aquelas piscadinhas
    //    {
    //        visibleCount += Time.deltaTime;
    //        if (visibleCount >= visibleTime)
    //        {
    //            isVisible = false;
    //            visibleCount = 0;
    //        }
    //    }



    //}



     



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("isJump", false);
        }


        if (collision.gameObject.tag == "checkpoint")
        {
            posInicial = collision.gameObject.transform.position; //pega a posição do player ao passar no checkpoint
            transform.position = posInicial;
            //Destroy(collision.gameObject);
        }




        if (collision.gameObject.layer == 10)

        {
           Controller.current.RemoveLife(1);
            // this.transform.position = spawnPoint.transform.position;
            //Destroy(gameObject);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gcc = GameObject.FindGameObjectWithTag("GC").GetComponent<GameControllerCheck>();
            transform.position = gcc.ultimoCheckpoint;

           
        }

       
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == 8)
    //    {
    //        isJumping = false;
    //        anim.SetBool("isJump", false);
    //    }
    //}


    public IEnumerator PlayerDemage(float DemageTime)
    {

        SpriteRend.enabled = false; //desativa o spriteRenderer
        yield return new WaitForSeconds(DemageTime); //depois de 3 segundos é executado o comando listado abaixo
        
        SpriteRend.enabled = true;
        yield return new WaitForSeconds(DemageTime);
        SpriteRend.enabled = true;
        yield return new WaitForSeconds(DemageTime);
        



        SpriteRend.enabled = true;

    }

    //public void Save()
    //{
    //    PlayerSave.SaveGame(this);

    //}

    //public void Load()
    //{
    //    PlayerSave.LoadPlayer(this);

    //}




}
