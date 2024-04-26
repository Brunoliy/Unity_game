using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InimigoMovimentacao : MonoBehaviour
{


    private Transform player;

    [SerializeField] private float raioVisao;
    [SerializeField] private LayerMask layerAreaVisao;



    [SerializeField] private int velocidade;
    [SerializeField] private Rigidbody2D rigidbody;

    [SerializeField] private float distanciaMinima;

    [SerializeField] private SpriteRenderer spriteRenderer;

    public void MexeBoneco()
    {

        Vector2 posicaoAlvo = this.player.position;
        Vector2 posicaoAtual = this.transform.position;

        float distancia = Vector2.Distance(posicaoAtual, posicaoAlvo);

        if (distancia >= distanciaMinima)
        {
            Vector2 direcao = posicaoAlvo - posicaoAtual;
            direcao = direcao.normalized;

            rigidbody.velocity = (velocidade * direcao);

            if (rigidbody.velocity.x > 0)
            {

                this.spriteRenderer.flipX = false;

            }
            else if (rigidbody.velocity.x < 0)
            {
                this.spriteRenderer.flipX = true;
            }
        }
        else
        {
            PararBoneco();
        }


    }

    public void PararBoneco()
    {

        rigidbody.velocity = Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, this.raioVisao);
    }

    public void ProcuraJogador()
    {
        Collider2D colisor = Physics2D.OverlapCircle(this.transform.position, this.raioVisao, this.layerAreaVisao);
        if (colisor != null)
        {
            this.player = colisor.transform;
            //colisor = 100;
        }
        else
        {
            this.player = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        ProcuraJogador();

        if (this.player != null)
        {
            MexeBoneco();
        }
        else
        {
            PararBoneco();
        }

    }
}
