using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class MultiJogador : MonoBehaviour
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
    [SerializeField] AnimacaoJogador animacaoJogador;
    private GameManagement gameManagement;
    [SerializeField] public int vidas;






    // Start is called before the first frame update
    private void Start()
    {
        this.direcaoMovimento = DirecaoMovimento.Direita;
        _rigidbody = GetComponent<Rigidbody2D>();

        // Encontre o GameManager na cena
        gameManagement = FindObjectOfType<GameManagement>();
        if (gameManagement == null)
        {
            Debug.LogWarning("GameManager não encontrado na cena!");
        }

        // Encontre o joystick na cena
        joystick = FindObjectOfType<FixedJoystick>();
        if (joystick == null)
        {
            Debug.LogWarning("Joystick não encontrado na cena!");
        }
    }

    private void Update()
    {
        this.vidas = this.gameManagement.vidas;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Derrotado)
        {

            return;

        }
        if (this.MultAtaqueJogador.Atacando)
        {
            //Parar a movimenta��o
            this._rigidbody.velocity = Vector2.zero;
            Debug.Log("Velocidade zerada");
        }
        else
        {
            float horizontal = this.joystick.Horizontal;
            float vertical = this.joystick.Vertical;
            keyboardHorizontal = Input.GetAxis("KeyboardHorizontal");
            keyboardVertical = Input.GetAxis("KeyboardVertical");


            Vector2 direcao = new(horizontal + keyboardHorizontal, vertical + keyboardVertical);
            direcao = direcao.normalized;
            Debug.Log(direcao + "=>" + direcao.magnitude);

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
        this.gameManagement.PerderVida();
        if (this.vidas < 0)
        {
            this.vidas = 0;
        }
        else
        {
            this.animacaoJogador.ReceberDano(Derrotado);
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
