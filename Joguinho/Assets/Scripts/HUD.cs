using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
	public GameObject[] vidas;

	public void DesativarVida(int indice)
	{
		vidas[indice].SetActive(false);
	}

	public void AtivarVida(int indice)
	{
		vidas[indice].SetActive(true);
	}
}
