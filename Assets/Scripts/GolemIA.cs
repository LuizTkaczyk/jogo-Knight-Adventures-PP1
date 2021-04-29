using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemIA : MonoBehaviour
{
    private Rigidbody2D rig;
    private int andandoDireita;
    public float velocidade;
    private float movimentoHorizontal;
    private Animator anim;
   

    public LayerMask layerPermitida;
    public Vector2 raycastOffset;
    private bool estaNoChao;
    private bool controleAereo = true;
    public float forcaDoPulo = 10f;
    private bool viradoParaDireita = true;
    private float suavizacaoMovimento = .05f;

    public float areaDeteccao;
    public bool modoPersegue;
    private bool seguindoPlayer;
    private bool estaPulando;
    public float puloInimigo;
    public float distanciaPlayer;
  
    [SerializeField]
    private Player player;





    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rig = gameObject.GetComponent<Rigidbody2D>();
        andandoDireita = 1;
    }

      void FixedUpdate()
    {
        
        Movimento(movimentoHorizontal * Time.fixedDeltaTime, false);
       
        
    }


    // Update is called once per frame
    void Update()
    {
        //segue o player
        var diferencaPlayer = player.gameObject.transform.position.x - transform.position.x + distanciaPlayer;
        seguindoPlayer = Mathf.Abs(diferencaPlayer) < areaDeteccao;
        
        if(modoPersegue && seguindoPlayer)
        {
            if(diferencaPlayer < 0)
            {
                andandoDireita = -1;
            }
            else
            {
                andandoDireita = 1;
            }
        }



        //movimento do inimigo
        movimentoHorizontal = andandoDireita * velocidade;


        
        var inicioX = transform.position.x + raycastOffset.x;
        var inicioY = transform.position.y + raycastOffset.y;
        
        
        //detecta a parede da direita e volta
        var raycastParedeDireita = Physics2D.Raycast(new Vector2(inicioX, inicioY), Vector2.right, 0.5f, layerPermitida);
        Debug.DrawRay(new Vector2(transform.position.x, inicioY), Vector2.right, Color.blue);
        if(raycastParedeDireita.collider != null)
        {
            if (!seguindoPlayer)
            {
                andandoDireita = -1;
            }
            else
            {
                Pula();
            }

          
          
        }

        //detecta a parede da esquerda e volta
        var raycastParedeEsquerda = Physics2D.Raycast(new Vector2(transform.position.x - raycastOffset.x, inicioY), Vector2.left, 0.4f, layerPermitida);
        Debug.DrawRay(new Vector2(transform.position.x, inicioY), Vector2.left, Color.blue);

        if (raycastParedeEsquerda.collider != null)
        {
            if (!seguindoPlayer)
            {
                andandoDireita = 1;
            }
            else
            {
                Pula();
            }
           
           
        }

        //detecta se não tem chão na direita e volta
        var raycastChaoDireita = Physics2D.Raycast(new Vector2(transform.position.x + raycastOffset.x, transform.position.y), Vector2.down, 1f, layerPermitida);
        Debug.DrawRay(new Vector2(transform.position.x + raycastOffset.x, transform.position.y), Vector2.down, Color.red);
        if(raycastChaoDireita.collider == null)
        {
            andandoDireita = -1;

        }

        //detecta se não houver chão na esquerda e volta
        var raycastChaoEsquerda = Physics2D.Raycast(new Vector2(transform.position.x - raycastOffset.x, transform.position.y), Vector2.down, 1f, layerPermitida);
        Debug.DrawRay(new Vector2(transform.position.x - raycastOffset.x, transform.position.y), Vector2.down, Color.red);
        if (raycastChaoEsquerda.collider == null)
        {
             andandoDireita = 1;
            
        }

    }

    void Pula()
    {
        

        rig.AddForce(Vector2.up * puloInimigo);
        anim.SetTrigger("pula");
        
        
    }

  

    public void Movimento(float qtdMovimento, bool pulando)
    {
        
        if (estaNoChao || controleAereo)
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
        rig.velocity = Vector3.SmoothDamp(rig.velocity, velocidadeJogador, ref velocity, suavizacaoMovimento);
        anim.SetTrigger("anda");
    }


    private void DetectaGirar(float qtdMovimento)
    {
        // Se a quantidade de movimento é maior que 0, significa que a velocidade aplicada é para a direita
        // Se o jogador estiver virado para a esquerda, devemos girar ele para a direita e vice-versa
        if (qtdMovimento > 0 && !viradoParaDireita)
        {
            GiraJogador();
        }
        else if (qtdMovimento < 0 && viradoParaDireita)
        {
            GiraJogador();
        }
    }

    private void GiraJogador()
    {
        // Troca o valor do boolean
        viradoParaDireita = !viradoParaDireita;

        // Multiplicar a escala local do jogador por -1 faz sempre com que ele gire 
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaDeteccao);
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
            velocidade = 0;

            Destroy(gameObject, 2f);

            GetComponent<CircleCollider2D>().enabled = false; //o colisor é desativado ao destruir o inimigo
            GetComponent<Rigidbody2D>().simulated = false;//desativa a massa do golem


        }
    }

}







