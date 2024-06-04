using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Vidas : MonoBehaviour
{

	[SerializeField] private string nomeDoLevelDeJogo;
	public Button button; // Referência ao botão
	public Text cooldownText; // Referência ao componente de texto para a contagem regressiva
	private bool isCooldown = false;
	private float cooldownTime = 5f;
	[SerializeField] private MultiJogador jogador;

	public void Recover()
	{
		string nomeDoLevelDeJogo = SceneManager.GetActiveScene().name;

		if (!isCooldown)
		{
			if (nomeDoLevelDeJogo == "GameplayMultiplayer")
			{
				MGameManagement gameManagement = MGameManagement.Instance;

				if (gameManagement != null)
				{
					bool vidaRecuperada = gameManagement.RecuperarVida();

					if (vidaRecuperada)
					{
						StartCoroutine(StartCooldown());
					}
				}
				else
				{
					Debug.LogWarning("MGameManagement não encontrado.");
				}
			}
			else
			{
				GameManagement.Instance.RecuperarVida();
				// Inicie o cooldown
				StartCoroutine(StartCooldown());
			}

		}
	}

	private IEnumerator StartCooldown()
	{
		isCooldown = true;
		float remainingTime = cooldownTime;
		while (remainingTime > 0)
		{
			cooldownText.text = $"{Mathf.CeilToInt(remainingTime)} s";
			yield return null;
			remainingTime -= Time.deltaTime;
		}
		isCooldown = false;
		cooldownText.text = "";
	}

}
