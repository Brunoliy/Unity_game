using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelPauseMenu;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelSair;
    [SerializeField] private GameObject painelObjetivo;
    [SerializeField] private GameObject painelHUD;
    [SerializeField] private GameObject painelDerrota;
    [SerializeField] private GameObject GameManager;



    public void ComecarJogo()
    {
        Time.timeScale = 1;
        GameManager.SetActive(true);
        painelObjetivo.SetActive(false);
        painelHUD.SetActive(true);
    }
    public void RecomecarJogo()
    {
        if (nomeDoLevelDeJogo == "Gameplay")
        {
            painelDerrota.SetActive(false);
            SceneManager.LoadScene("Gameplay");
            Time.timeScale = 0;
        }
        else if (nomeDoLevelDeJogo == "GameplayDificil")
        {
            painelDerrota.SetActive(false);
            SceneManager.LoadScene("GameplayDificil");
            Time.timeScale = 0;
        }
        else if (nomeDoLevelDeJogo == "GameplayNormal")
        {
            painelDerrota.SetActive(false);
            SceneManager.LoadScene("GameplayNormal");
            Time.timeScale = 0;
        }
        else if (nomeDoLevelDeJogo == "GameplayExpert")
        {
            painelDerrota.SetActive(false);
            SceneManager.LoadScene("GameplayExpert");
            Time.timeScale = 0;
        }
        else if (nomeDoLevelDeJogo == "GameplayMultiplayer")
        {
            painelDerrota.SetActive(false);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.LoadLevel("GameplayMultiplayer");
            Time.timeScale = 1;
        }


    }
    public void PauseGame()
    {
        if (nomeDoLevelDeJogo == "GameplayMultiplayer")
        {
            painelPauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            painelPauseMenu.SetActive(true);
        }
    }
    public void Resume()
    {
        if (nomeDoLevelDeJogo == "GameplayMultiplayer")
        {
            painelPauseMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            painelPauseMenu.SetActive(false);
        }
    }
    public void Opcoes()
    {
        painelPauseMenu.SetActive(false);
        painelOpcoes.SetActive(true);
    }
    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelPauseMenu.SetActive(true);
    }
    public void Sair()
    {
        painelPauseMenu.SetActive(false);
        painelSair.SetActive(true);

    }
    public void NaoSair()
    {
        painelSair.SetActive(false);
        painelPauseMenu.SetActive(true);

    }
    public void MenuInicial()
    {
        if (nomeDoLevelDeJogo == "GameplayMultiplayer")
        {
            Photon.Pun.PhotonNetwork.LeaveRoom();
            Photon.Pun.PhotonNetwork.Disconnect();
        }
        SceneManager.LoadScene("Menu");
    }


}
