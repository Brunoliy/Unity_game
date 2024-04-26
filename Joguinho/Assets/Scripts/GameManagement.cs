using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
	public static GameManagement Instance { get; private set; }
	[SerializeField] private GameObject painelDerrota;
	[SerializeField] private GameObject painelHUD;
	public MultiJogador jogador;
	public HUD hud;

	public int vidas = 6;

	private void Start()
	{
		//this.vidas = this.jogador.vidas;
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

	}
	public void PerderVida()
	{
		vidas -= 1;

		if (vidas == 0)
		{
			Time.timeScale = 0;
			painelHUD.SetActive(false);
			painelDerrota.SetActive(true);
			Debug.Log("Morreu");
		}
		hud.DesativarVida(vidas);
	}

	public bool RecuperarVida()
	{
		if (vidas == 6)
		{
			return false;
		}

		hud.AtivarVida(vidas);
		vidas += 1;
		return true;
	}
}
