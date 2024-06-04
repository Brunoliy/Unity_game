using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class AnimacaoJogador : MonoBehaviourPun
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    [SerializeField] private string nomeDoLevelDeJogo;


    private void LateUpdate()
    {
        Vector2 velocidade = this._rigidbody2D.velocity;

        if ((velocidade.x != 0) || (velocidade.y != 0))
        {
            this.animator.SetBool("movendo", true);
        }
        else
        {
            this.animator.SetBool("movendo", false);
        }

        if (velocidade.x > 0)
        {
            this.spriteRenderer.flipX = false;
        }
        else if (velocidade.x < 0)
        {
            this.spriteRenderer.flipX = true;
        }
    }
    public void Atacar()
    {
        this.animator.SetTrigger("ataqueEspada");
    }
    public void ReceberDano(bool derrotado)
    {
        if (nomeDoLevelDeJogo == "GameplayMultiplayer")
        {
            photonView.RPC("ReceberDanoRPC", RpcTarget.All, derrotado);
        }
        else
        {
            if (derrotado)
            {
                this.animator.SetBool("derrotado", true);
                GameObject.Destroy(this.gameObject, 1f);
            }
            else
            {
                this.animator.SetTrigger("recebendoDano");
            }
        }
    }

    [PunRPC]

    private void ReceberDanoRPC(bool derrotado)
    {
        if (derrotado)
        {
            this.animator.SetBool("derrotado", true);
            GameObject.Destroy(this.gameObject, 1f);

        }
        else
        {
            this.animator.SetTrigger("recebendoDano");
        }
    }
}
