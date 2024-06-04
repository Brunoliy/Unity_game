using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MGameManagement : MonoBehaviourPun
{
	public static MGameManagement Instance { get; private set; }
	[SerializeField] private GameObject painelDerrota;
	[SerializeField] private GameObject painelJogadorMorreu;
	[SerializeField] private GameObject painelHUD;
	public HUD hud;
	public int vidas = 6;

	private List<MultiJogador> jogadores = new List<MultiJogador>();

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
			Debug.Log("Cuidado! Mais de um GameManagement nesta cena");
		}
	}

	private void Start()
	{
		if (painelHUD == null)
		{
			painelHUD = GameObject.Find("PainelHUD");
			if (painelHUD == null)
			{
				Debug.LogError("Painel HUD não foi encontrado!");
			}
		}

		if (painelDerrota == null)
		{
			painelDerrota = GameObject.Find("PainelDerrota");
			if (painelDerrota == null)
			{
				Debug.LogError("Painel Derrota não foi encontrado!");
			}
		}

		if (hud == null)
		{
			hud = FindObjectOfType<HUD>();
			if (hud == null)
			{
				Debug.LogError("HUD não foi encontrado!");
			}
		}
	}
	public void AdicionarJogador(MultiJogador jogador)
	{
		jogadores.Add(jogador);
	}

	public void PerderVida(MultiJogador jogador)
	{
		vidas -= 1;
		hud.DesativarVida(jogador.vidas);

		if (jogador.vidas == 0)
		{
			ExibirDerrota();
		}
	}

	public bool RecuperarVida()
	{
		if (vidas >= 10)
		{
			return false;
		}

		vidas++;
		hud?.AtivarVida(vidas);
		return true;
	}



	private bool TodosJogadoresDerrotados()
	{

		foreach (var jogador in jogadores)
		{
			if (!jogador.Derrotado)
			{
				return false;
			}
		}
		return true;

	}
	private void ExibirDerrota()
	{
		if (painelHUD != null)
		{
			painelHUD.SetActive(false);
		}
		if (painelJogadorMorreu != null)
		{
			painelJogadorMorreu.SetActive(true);
		}
	}
}
