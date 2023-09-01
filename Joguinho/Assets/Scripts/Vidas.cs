using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vidas : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D jogador)
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