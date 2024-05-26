using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vidas : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D jogador)
	{
		if (jogador.gameObject.CompareTag("Player"))
		{
			bool vidaRecuperada = MGameManagement.Instance.RecuperarVida(jogador.GetComponent<MultiJogador>());

			if (vidaRecuperada)
			{
				Destroy(this.gameObject);
			}

		}
	}
}
