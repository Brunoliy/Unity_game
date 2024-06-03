using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (alvo == null)
        {
            Debug.LogError("Alvo não atribuído.");
            return;
        }

        string sceneName = SceneManager.GetActiveScene().name;
        object jogador = null;

        if (sceneName == "GameplayMultiplayer")
        {
            jogador = alvo.GetComponent<MultiJogador>();
            if (jogador == null)
            {
                Debug.LogError("Componente MultiJogador não encontrado no alvo.");
                return;
            }
        }
        else if (sceneName == "Gameplay" || sceneName == "GameplayNormal" || sceneName == "GameplayDificil")
        {
            jogador = alvo.GetComponent<Jogador>();
            if (jogador == null)
            {
                Debug.LogError("Componente Jogador não encontrado no alvo.");
                return;
            }
        }

        if ((jogador is MultiJogador multiJogador && multiJogador.Derrotado) ||
            (jogador is Jogador singleJogador && singleJogador.Derrotado))
        {
            return; // Interrompe o código se o jogador está derrotado
        }

        float distancia = Vector3.Distance(this.transform.position, this.alvo.position);
        if (distancia <= this.distanciaMaximaAtaque)
        {
            this.tempoEsperaAtaques -= Time.deltaTime;
            if (tempoEsperaAtaques <= 0)
            {
                this.tempoEsperaAtaques = this.intervaloAtaques; // Reinicia contagem de espera
                Atacar(); // Ataca o jogador
            }
        }
    }

    private void Atacar()
    {
        this.animatorInimigo.SetTrigger("atacando");
    }

    public void AplicarDano()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        /*if (sceneName == "GameplayMultiplayer")
        {
            MultiJogador jogador = alvo.GetComponent<MultiJogador>();
            if (jogador != null)
            {
                jogador.ReceberDano();
                return;
            }
        }
        else if (sceneName == "Gameplay" || sceneName == "GameplayNormal" || sceneName == "GameplayDificil")
        {
            Jogador jogador = alvo.GetComponent<Jogador>();
            if (jogador != null)
            {
                jogador.ReceberDano();
                return;
            }
        }*/

        //Obtém o componente de jogador associado ao alvo
        Jogador jogador = alvo.GetComponent<Jogador>();

        // Se o componente de jogador for encontrado, aplica dano ao jogador
        if (jogador != null)
        {
            jogador.ReceberDano();
            return;
        }

        // Obtém o componente de jogador multiplayer associado ao alvo
        MultiJogador jogadorMulti = alvo.GetComponent<MultiJogador>();

        // Se o componente de jogador multiplayer for encontrado, aplica dano ao jogador multiplayer
        if (jogadorMulti != null)
        {
            jogadorMulti.ReceberDano();
            return;
        }

        // Se nenhum dos componentes for encontrado, mostra um erro
        Debug.LogError("Componente de jogador não encontrado no alvo.");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, this.raioVisao);
        if (this.alvo != null)
        {
            Gizmos.DrawLine(this.transform.position, this.alvo.position);
        }
    }

    public void ProcuraJogador()
    {
        Collider2D colisor = Physics2D.OverlapCircle(this.transform.position, this.raioVisao, this.layerAreaVisao);
        if (colisor != null)
        {
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

            this._rigidbody.velocity = this.velocidadeMovimento * direcao;

            this.spriteRenderer.flipX = this._rigidbody.velocity.x < 0;
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
            Debug.Log(this.name + " recebeu dano. Vida: " + this.vidas);
            if (this.vidas <= 0)
            {
                Debug.Log("Inimigo Morreu");
                this.animatorInimigo.SetBool("derrotado", true);
                Destroy(this.gameObject, 0.5f);
            }
            else
            {
                this.animatorInimigo.SetTrigger("recebendoDano");
            }
        }
    }
}
