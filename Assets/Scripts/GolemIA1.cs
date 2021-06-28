using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemIA1 : MonoBehaviour
{

    //Código do golem com IA 

    private Rigidbody2D rig;
    private int walkRight;
    public float velocity;
    private float horizontalMovement;
    private Animator anim;
    public LayerMask allowedLayer;
    public Vector2 raycastOffset;
    private bool onTheFloor;
    private bool airControl = true;
    public float jumpForce = 10f;
    private bool scrolledToRight = true;
    private float smoothingMovement = .05f;
    public float detectionArea;
    public bool chaseMode;
    private bool chasePlayer;
    private bool isJump;
    public float enemyJump;
    public float playerDistance;

    [SerializeField]
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rig = gameObject.GetComponent<Rigidbody2D>();
        walkRight = 1;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        Movimento(horizontalMovement * Time.fixedDeltaTime, false);
    }


    // Update is called once per frame
    void Update()
    {

        if (player.GetComponent<Player>().isAlive)
        {
            //segue o player
            var diferencaPlayer = player.gameObject.transform.position.x - transform.position.x + playerDistance;
            chasePlayer = Mathf.Abs(diferencaPlayer) < detectionArea;

            if (chaseMode && chasePlayer)
            {
                if (diferencaPlayer < 0)
                {
                    walkRight = -1;

                }
                else
                {
                    walkRight = 1;

                }
            }
        }

        //movimento do inimigo
        horizontalMovement = walkRight * velocity;
        var inicioX = transform.position.x + raycastOffset.x;
        var inicioY = transform.position.y + raycastOffset.y;
        
        //detecta a parede da direita e volta
        var raycastParedeDireita = Physics2D.Raycast(new Vector2(inicioX, inicioY), Vector2.right, 0.5f, allowedLayer);
        Debug.DrawRay(new Vector2(transform.position.x, inicioY), Vector2.right, Color.blue);
        if (raycastParedeDireita.collider != null)
        {
            if (!chasePlayer)
            {

                walkRight = -1;
            }

        }

        //detecta a parede da esquerda e volta
        var raycastParedeEsquerda = Physics2D.Raycast(new Vector2(transform.position.x - raycastOffset.x, inicioY), Vector2.left, 0.4f, allowedLayer);
        Debug.DrawRay(new Vector2(transform.position.x, inicioY), Vector2.left, Color.blue);

        if (raycastParedeEsquerda.collider != null)
        {
            if (!chasePlayer)
            {
                walkRight = 1;
            }

        }

        //detecta se não tem chão na direita e volta
        var raycastChaoDireita = Physics2D.Raycast(new Vector2(transform.position.x + raycastOffset.x, transform.position.y), Vector2.down, 1f, allowedLayer);
        Debug.DrawRay(new Vector2(transform.position.x + raycastOffset.x, transform.position.y), Vector2.down, Color.red);
        if (raycastChaoDireita.collider == null)
        {
            walkRight = -1;

        }

        //detecta se não houver chão na esquerda e volta
        var raycastChaoEsquerda = Physics2D.Raycast(new Vector2(transform.position.x - raycastOffset.x, transform.position.y), Vector2.down, 1f, allowedLayer);
        Debug.DrawRay(new Vector2(transform.position.x - raycastOffset.x, transform.position.y), Vector2.down, Color.red);
        if (raycastChaoEsquerda.collider == null)
        {
            walkRight = 1;

        }

    }
    
    public void Movimento(float qtdMovimento, bool pulando)
    {

        if (onTheFloor || airControl)
        {
            AplicaMovimento(qtdMovimento);
            DetectaGirar(qtdMovimento);
        }

    }


    private void AplicaMovimento(float qtdMovimento)
    {
        // Encontra a velocidade do jogador
        var velocidadeJogador = new Vector2(qtdMovimento * 10f, rig.velocity.y);
        // Suavizando a velocidade de movimento
        Vector3 velocity = Vector3.zero;
        rig.velocity = Vector3.SmoothDamp(rig.velocity, velocidadeJogador, ref velocity, smoothingMovement);
        anim.SetTrigger("anda");
    }


    private void DetectaGirar(float qtdMovimento)
    {
        // Se a quantidade de movimento é maior que 0, significa que a velocidade aplicada é para a direita
        // Se o jogador estiver virado para a esquerda, devemos girar ele para a direita e vice-versa
        if (qtdMovimento > 0 && !scrolledToRight)
        {
            GiraJogador();
        }
        else if (qtdMovimento < 0 && scrolledToRight)
        {
            GiraJogador();
        }
    }

    private void GiraJogador()
    {
        // Troca o valor do boolean
        scrolledToRight = !scrolledToRight;

        // Multiplicar a escala local do jogador por -1 faz sempre com que ele gire 
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionArea);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player") // se o inimigo bater no player , o inimigo ataca
        {
            anim.SetTrigger("ataca");

            Debug.Log("acertou");

            if (!collision.gameObject.GetComponent<Player>().isVisible)
            {
                collision.gameObject.transform.Translate(-Vector2.right * 0.5f); // força do empurrão do inimigo
                Controller.current.RemoveLife(1); //perde uma vida
                StartCoroutine(collision.gameObject.GetComponent<Player>().PlayerDemage(0.05f));
                collision.gameObject.GetComponent<Player>().isVisible = true;
            }
        }

        if (collision.gameObject.layer == 9) //layer do machado !
        {
            anim.SetTrigger("morre");
            velocity = 0;
            Destroy(gameObject, 2f);
            GetComponent<CircleCollider2D>().enabled = false; //o colisor é desativado ao destruir o inimigo
            GetComponent<Rigidbody2D>().simulated = false;//desativa a massa do golem
        }
    }

}







