using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MultAtaqueJogador : MonoBehaviour
{
    [SerializeField] private Transform pontoAtaqueDireita;
    [SerializeField] private Transform pontoAtaqueEsquerda;
    [SerializeField] private float raioAtaque;
    [SerializeField] private LayerMask layersAtaque;

    [SerializeField] private MultiJogador Jogador;
    [SerializeField] private AnimacaoJogador animacaoJogador;

    private bool atacando;

    private Button botaoAtaque;

    void Start()
    {
        this.atacando = false;
        //botaoAtaque.onClick.AddListener(VerificarAtaque);
        // Encontre o botão de ataque na cena


        // Encontre o objeto "UI" na cena
        // Encontre o objeto "UI" na cena
        GameObject uiObject = GameObject.Find("UI");
        if (uiObject != null)
        {
            // Encontre o objeto "HUD" dentro do objeto "UI"
            Transform hudTransform = uiObject.transform.Find("HUD");
            if (hudTransform != null)
            {
                // Encontre o botão de ataque dentro do objeto "HUD" pelo nome
                Transform attackButtonTransform = hudTransform.Find("AttackButton");
                if (attackButtonTransform != null)
                {
                    // Obtenha o componente Button do botão de ataque
                    Button botaoAtaque = attackButtonTransform.GetComponent<Button>();
                    if (botaoAtaque != null)
                    {
                        // Adicione um listener para o botão de ataque
                        botaoAtaque.onClick.AddListener(VerificarAtaque);
                    }
                    else
                    {
                        Debug.LogWarning("Componente Button não encontrado em AttackButton!");
                    }
                }
                else
                {
                    Debug.LogWarning("AttackButton não encontrado em HUD!");
                }
            }
            else
            {
                Debug.LogWarning("Objeto HUD não encontrado em UI!");
            }
        }
        else
        {
            Debug.LogWarning("Objeto UI não encontrado na cena!");
        }

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
        get
        {
            return this.atacando;
        }
    }

    public void Atacar()
    {
        this.atacando = true;
        //Executar anima��o de ataque
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
        if (collidersInimigos != null)
        {
            foreach (Collider2D colliderInimigo in collidersInimigos)
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
