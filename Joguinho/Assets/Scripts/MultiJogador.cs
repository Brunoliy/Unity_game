using UnityEngine;
using Photon.Pun;
using System.Data.Common;
using Unity.VisualScripting;

public class MultiJogador : MonoBehaviourPun
{
    private FixedJoystick joystick;
    private float keyboardHorizontal;
    private float keyboardVertical;
    public DirecaoMovimento direcaoMovimento;

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float velocidadeMovimento;
    [SerializeField] private MultAtaqueJogador MultAtaqueJogador;
    [SerializeField] private AnimacaoJogador animacaoJogador;
    private MGameManagement gameManagement;
    [SerializeField] public int vidas;

    private bool derrotado;

    private void Start()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        this.direcaoMovimento = DirecaoMovimento.Direita;
        _rigidbody = GetComponent<Rigidbody2D>();

        gameManagement = MGameManagement.Instance;
        if (gameManagement == null)
        {
            Debug.LogWarning("GameManager não encontrado na cena!");
        }
        else
        {
            gameManagement.AdicionarJogador(this);
        }

        joystick = FindObjectOfType<FixedJoystick>();
        if (joystick == null)
        {
            Debug.LogWarning("Joystick não encontrado na cena!");
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine || Derrotado)
        {
            return;
        }

        if (this.MultAtaqueJogador.Atacando)
        {
            this._rigidbody.velocity = Vector2.zero;
            Debug.Log("Velocidade zerada");
        }
        else
        {
            float horizontal = this.joystick.Horizontal;
            float vertical = this.joystick.Vertical;
            keyboardHorizontal = Input.GetAxis("KeyboardHorizontal");
            keyboardVertical = Input.GetAxis("KeyboardVertical");

            Vector2 direcao = new Vector2(horizontal + keyboardHorizontal, vertical + keyboardVertical).normalized;

            if (direcao.x > 0)
            {
                this.direcaoMovimento = DirecaoMovimento.Direita;
            }
            else if (direcao.x < 0)
            {
                this.direcaoMovimento = DirecaoMovimento.Esquerda;
            }

            this._rigidbody.velocity = direcao * this.velocidadeMovimento;
        }
    }

    public void ReceberDano()
    {
        this.vidas--;
        Debug.Log($"Jogador {photonView.Owner.NickName} recebeu dano. Vidas restantes: {this.vidas}");

        if (this.vidas <= 0)
        {
            this.vidas = 0;
            Derrotado = true;
        }
        else
        {
            this.animacaoJogador.ReceberDano(Derrotado);
        }

        gameManagement.PerderVida(this);
    }

    public bool Derrotado
    {
        get
        {
            return derrotado;
        }
        set
        {
            derrotado = value;
        }
    }

    public void Reviver()
    {
        this.Derrotado = false;
        this.vidas = 6;
        this.photonView.RPC("RPCReviver", RpcTarget.All);
    }

    [PunRPC]
    public void RPCReviver()
    {
        Derrotado = false;
        this.vidas = 6;
    }
}
