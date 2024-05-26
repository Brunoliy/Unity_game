using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MGameManagement : MonoBehaviourPun
{
	public static MGameManagement Instance { get; private set; }
	[SerializeField] private GameObject painelDerrota;
	[SerializeField] private GameObject painelHUD;
	public HUD hud;
	private List<MultiJogador> jogadores = new List<MultiJogador>();

	private void Start()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Debug.Log("Cuidado! Mais de um GameManagement nesta cena");
		}
	}

	private void Update()
	{


		// Coloque aqui qualquer código que deve ser executado no Update apenas pelo proprietário.
	}

	public void AdicionarJogador(MultiJogador jogador)
	{
		jogadores.Add(jogador);
	}

	public void PerderVida(MultiJogador jogador)
	{
		if (!photonView.IsMine)
		{
			return;
		}

		jogador.vidas -= 1;
		hud.DesativarVida(jogador.vidas);

		if (jogador.vidas == 0 && TodosJogadoresDerrotados())
		{
			Time.timeScale = 0;
			painelHUD.SetActive(false);
			painelDerrota.SetActive(true);
			Debug.Log("Morreu");
		}
	}

	private bool TodosJogadoresDerrotados()
	{
		foreach (MultiJogador jogador in jogadores)
		{
			if (jogador != null && !jogador.Derrotado)
			{
				return false;
			}
		}
		return true;
	}

	public bool RecuperarVida(MultiJogador jogador)
	{
		if (!photonView.IsMine)
		{
			return false;
		}

		if (jogador.vidas == 6)
		{
			return false;
		}

		hud.AtivarVida(jogador.vidas);
		jogador.vidas += 1;
		return true;
	}
}
