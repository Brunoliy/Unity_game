using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Vidas : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D jogador)
	{
		string sceneName = SceneManager.GetActiveScene().name;
		if (sceneName == "GameplayMultiplayer")
		{
			if (jogador.gameObject.CompareTag("Player"))
			{
				bool vidaRecuperada = MGameManagement.Instance.RecuperarVida();

				if (vidaRecuperada)
				{
					Destroy(this.gameObject);
				}

			}
		}
		else
		{
			if (jogador.gameObject.CompareTag("Player"))
			{
				bool vidaRecuperada = GameManagement.Instance.RecuperarVida();

				if (vidaRecuperada)
				{
					Destroy(this.gameObject);
				}

			}
		}
	}
}
