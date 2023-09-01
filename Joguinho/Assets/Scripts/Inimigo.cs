using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inimigo : MonoBehaviour
{
   
    private Transform alvo;

    [SerializeField] private float raioVisao;

    [SerializeField] private LayerMask layerAreaVisao;

    [SerializeField] private float distanciaMaximaAtaque;

    [SerializeField] private float intervaloAtaques;

    private float tempoEsperaAtaques;

    [SerializeField] private float velocidadeMovimento;

    [SerializeField] private float distanciaMinima;

    [SerializeField] Rigidbody2D _rigidbody;

    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] public int vidas;

    [SerializeField] private Animator animatorInimigo;

    [SerializeField] private BarraVida barraVida;
    
    
    private void Start()
    {
        this.barraVida.VidaMaxima = this.vidas;
        this.barraVida.Vida = this.vidas;
        this.tempoEsperaAtaques = this.intervaloAtaques;
        animatorInimigo = GetComponent<Animator>();
    }

    private void Update()
    {
        ProcuraJogador();

        if (this.alvo != null)
        {
            Mover();
            VerificarAtaque();
        }
        else
        {
            PararMovimentacao();
        }
    }

    private void VerificarAtaque()
    {
        Jogador jogador = this.alvo.GetComponent<Jogador>();
        if (jogador.Derrotado)
        {
            return; //Retorna vazio, assim interrompendo o codigo
        }
        float distancia = Vector3.Distance(this.transform.position, this.alvo.position);
        if(distancia <= this.distanciaMaximaAtaque)
        {
            this.tempoEsperaAtaques -= Time.deltaTime;
            if(tempoEsperaAtaques <= 0)
            {
                this.tempoEsperaAtaques = this.intervaloAtaques; //Reiniciar contagem de espera
              
                Atacar(); //Ataca o jogador
            }


        }
    }
    private void Atacar()
    {
        this.animatorInimigo.SetTrigger("atacando"); 
    }
     public void AplicarDano()
    {
        Jogador jogador = this.alvo.GetComponent<Jogador>();
        jogador.ReceberDano();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, this.raioVisao);
        if(this.alvo != null)
        {
            Gizmos.DrawLine(this.transform.position, this.alvo.position);
        }
    }

    public void ProcuraJogador()
    {
        Collider2D colisor = Physics2D.OverlapCircle(this.transform.position, this.raioVisao, this.layerAreaVisao);
        if (colisor != null)
        {
            Vector2 posicaoAtual = this.transform.position;
            Vector2 posicaoAlvo = colisor.transform.position;
            Vector2 direcao = posicaoAlvo - posicaoAtual;
            direcao = direcao.normalized;
            this.alvo = colisor.transform;
        }
        else
        {
            this.alvo = null;
        }
    }

    private void Mover()
    {
        Vector2 posicaoAlvo = this.alvo.position;
        Vector2 posicaoAtual = this.transform.position;

        float distancia = Vector2.Distance(posicaoAtual, posicaoAlvo);
        if (distancia >= this.distanciaMinima)
        {
            Vector2 direcao = posicaoAlvo - posicaoAtual;
            direcao = direcao.normalized;

            this._rigidbody.velocity = (this.velocidadeMovimento * direcao);

            if (this._rigidbody.velocity.x > 0)
            {
                this.spriteRenderer.flipX = false;
            }
            else if (this._rigidbody.velocity.x < 0)
            {
                this.spriteRenderer.flipX = true;
            }
        }
        else
        {

            PararMovimentacao();
        }
    }
    private void PararMovimentacao()
    {
        this._rigidbody.velocity = Vector2.zero;
    }
    public void ReceberDano()
    {
        if (this.vidas > 0)
        {
            this.vidas--;

            this.barraVida.Vida = this.vidas;
            Debug.Log(this.name + "recebeu dano. Vida:" + this.vidas);
            if (this.vidas <= 0)
            {
                Debug.Log("Inimigo Morreu");
                this.animatorInimigo.SetBool("derrotado", true);
                GameObject.Destroy(this.gameObject, 0.5f);
            }
            else
            {
                this.animatorInimigo.SetTrigger("recebendoDano");
            }
        }
    }
}
