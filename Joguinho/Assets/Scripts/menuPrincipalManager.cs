using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class menuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelJogar;
    [SerializeField] private GameObject painelMultiplayer;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelCreditos;
    [SerializeField] private Button BotaoFacil;
    [SerializeField] private Button BotaoNormal;
    [SerializeField] private Button BotaoDificil;
   

    public void AbrirJogar()
    {
        painelMenuInicial.SetActive(false);
        painelJogar.SetActive(true);

    }

    public void JogarFacil()
    {
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 0;
    }public void AbrirMultiplayer()
    {
        painelMenuInicial.SetActive(false);
        painelMultiplayer.SetActive(true);
    }
    public void JogarNormal()
    {
        SceneManager.LoadScene("GameplayNormal");
        Time.timeScale = 0;
    }
    public void JogarDificil()
    {
        SceneManager.LoadScene("GameplayDificil");
        Time.timeScale = 0;
    }

    public void FecharJogar()
    {
        painelJogar.SetActive(false);
        painelMenuInicial.SetActive(true);

    }

    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);

    }

    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);


    }
    public void AbrirCreditos()
    {
        painelMenuInicial.SetActive(false);
        painelCreditos.SetActive(true);

    }
    public void FecharCreditos()
    {
        painelCreditos.SetActive(false);
        painelMenuInicial.SetActive(true);


    }

    public void SairJogo()
    {
        Debug.Log("Sair Do Jogo");
        Application.Quit();

    }
}


