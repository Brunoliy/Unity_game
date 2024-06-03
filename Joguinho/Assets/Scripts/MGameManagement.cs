using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MGameManagement : MonoBehaviourPun
{
	public static MGameManagement Instance { get; private set; }
	[SerializeField] private GameObject painelDerrota;
	[SerializeField] private GameObject painelHUD;
	public HUD hud;
	public int vidas = 6;

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

	public void PerderVida()
	{
		photonView.RPC("PerderVidaRPC", RpcTarget.All);
	}

	[PunRPC]
	public void PerderVidaRPC()
	{
		vidas -= 1;

		if (hud != null)
		{
			hud.DesativarVida(vidas);
		}
		else
		{
			Debug.LogWarning("HUD não está atribuído no MGameManagement.");
		}

		if (vidas <= 0)
		{
			Time.timeScale = 0;
			if (painelHUD != null)
			{
				painelHUD.SetActive(false);
			}
			if (painelDerrota != null)
			{
				painelDerrota.SetActive(true);
			}
			Debug.Log("Morreu");
		}
	}

	public bool RecuperarVida()
	{
		if (vidas == 6)
		{
			return false;
		}

		if (hud != null)
		{
			hud.AtivarVida(vidas);
		}
		else
		{
			Debug.LogWarning("HUD não está atribuído no MGameManagement.");
		}
		vidas += 1;
		return true;
	}
}
