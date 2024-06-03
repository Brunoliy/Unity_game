using UnityEngine;
using Photon.Pun;

public class MultiJogador : MonoBehaviourPun
{
    private FixedJoystick joystick;
    private float keyboardHorizontal;
    private float keyboardVertical;
    public DirecaoMovimento direcaoMovimento;
    public TextMesh textoNome;
    public PhotonView photonView;

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float velocidadeMovimento;
    [SerializeField] private MultAtaqueJogador MultAtaqueJogador;
    [SerializeField] private AnimacaoJogador animacaoJogador;
    private MGameManagement gameManagement;
    [SerializeField] public int vidas;

    private void Start()
    {
        this.direcaoMovimento = DirecaoMovimento.Direita;
        _rigidbody = GetComponent<Rigidbody2D>();

        gameManagement = MGameManagement.Instance;
        if (gameManagement == null)
        {
            Debug.LogWarning("GameManager não encontrado na cena!");
        }

        joystick = FindObjectOfType<FixedJoystick>();
        if (joystick == null)
        {
            Debug.LogWarning("Joystick não encontrado na cena!");
        }
    }

    private void Update()
    {
        if (gameManagement != null)
        {
            this.vidas = gameManagement.vidas;
        }
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine || Derrotado)
        {
            return;
        }

        if (MultAtaqueJogador != null && MultAtaqueJogador.Atacando)
        {
            _rigidbody.velocity = Vector2.zero;
            Debug.Log("Velocidade zerada");
        }
        else
        {
            float horizontal = joystick != null ? joystick.Horizontal : 0f;
            float vertical = joystick != null ? joystick.Vertical : 0f;
            keyboardHorizontal = Input.GetAxis("KeyboardHorizontal");
            keyboardVertical = Input.GetAxis("KeyboardVertical");

            Vector2 direcao = new Vector2(horizontal + keyboardHorizontal, vertical + keyboardVertical).normalized;

            if (direcao.x > 0)
            {
                direcaoMovimento = DirecaoMovimento.Direita;
            }
            else if (direcao.x < 0)
            {
                direcaoMovimento = DirecaoMovimento.Esquerda;
            }

            _rigidbody.velocity = direcao * velocidadeMovimento;
        }
    }

    public void ReceberDano()
    {
        photonView.RPC("ReceberDanoRPC", RpcTarget.All);
    }

    [PunRPC]
    public void ReceberDanoRPC()
    {
        this.vidas--;
        Debug.Log($"Jogador {photonView.Owner.NickName} recebeu dano. Vidas restantes: {this.vidas}");

        if (gameManagement != null)
        {
            gameManagement.PerderVida();
        }
        else
        {
            Debug.LogWarning("GameManagement não está atribuído no MultiJogador.");
        }

        if (this.vidas <= 0)
        {
            this.vidas = 0;
        }
        else if (animacaoJogador != null)
        {
            animacaoJogador.ReceberDano(Derrotado);
        }
    }

    public bool Derrotado
    {
        get
        {
            return (this.vidas <= 0);
        }
    }
}
