using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class AtaqueJogador : MonoBehaviour
{
    [SerializeField] private Transform pontoAtaqueDireita;
    [SerializeField] private Transform pontoAtaqueEsquerda;
    [SerializeField] private float raioAtaque;
    [SerializeField] private LayerMask layersAtaque;

    [SerializeField] private Jogador Jogador;
    [SerializeField] private AnimacaoJogador animacaoJogador;

    private bool atacando;

    public Button botaoAtaque;
   
    void Start()
    {
        this.atacando = false;
        botaoAtaque.onClick.AddListener(VerificarAtaque);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && this.atacando == false)
        {
            Atacar();
        }
    }

    private void VerificarAtaque()
    {
        if (!atacando)
        {
            Atacar();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        //Cria uma esfera para definir a area do ataque
        if (this.pontoAtaqueDireita != null)
        {
            Gizmos.DrawWireSphere(this.pontoAtaqueDireita.position, this.raioAtaque);
        }
        if (this.pontoAtaqueEsquerda != null)
        {
            Gizmos.DrawWireSphere(this.pontoAtaqueEsquerda.position, this.raioAtaque);
        }

        Transform pontoAtaque;
        if (this.Jogador.direcaoMovimento == DirecaoMovimento.Direita)
        {
            pontoAtaque = this.pontoAtaqueDireita;
        }
        else
        {
            pontoAtaque = this.pontoAtaqueEsquerda;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pontoAtaque.position, this.raioAtaque);
    }

    public bool Atacando
    {
        get{
            return this.atacando;
        }
    }

    public void Atacar()
    {
        this.atacando = true;
        //Executar animação de ataque
        this.animacaoJogador.Atacar();
        //Aplica dano nos inimigos
        Transform pontoAtaque;
        if (this.Jogador.direcaoMovimento == DirecaoMovimento.Direita)
        {
            pontoAtaque = this.pontoAtaqueDireita;
        }
        else
        {
            pontoAtaque = this.pontoAtaqueEsquerda;
        }

        Debug.Log("Atacando");
        Collider2D[] collidersInimigos = Physics2D.OverlapCircleAll(pontoAtaque.position, this.raioAtaque, this.layersAtaque);
        if(collidersInimigos != null)
        {
            foreach(Collider2D colliderInimigo in collidersInimigos)
            {
                Debug.Log("Atacando objeto" + colliderInimigo.name);
                //Causar dano no inimigo
                Inimigo inimigo = colliderInimigo.GetComponent<Inimigo>();

                if (inimigo != null)
                {
                    inimigo.ReceberDano();
                }
            }
        }
    }
    public void ComecarAtaque()
    {
        this.atacando = true;
    }
    public void EncerrarAtaque()
    {
        this.atacando = false;
    }
}
