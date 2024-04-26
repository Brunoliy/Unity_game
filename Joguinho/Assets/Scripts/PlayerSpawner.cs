using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;


    private void Start()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumber];
        if (PhotonNetwork.LocalPlayer == null)
        {
            Debug.LogError("PhotonNetwork.LocalPlayer é nulo. Certifique-se de estar conectado a uma sala Photon.");
            return;
        }

        if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("playerAvatar"))
        {
            Debug.LogError("A propriedade 'playerAvatar' não está definida para o jogador local.");
            return;
        }

        if (playerPrefabs == null || playerPrefabs.Length == 0)
        {
            Debug.LogError("playerPrefabs está vazio. Verifique se você atribuiu os prefabs de jogador no Editor do Unity.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("spawnPoints está vazio. Verifique se você atribuiu os pontos de spawn no Editor do Unity.");
            return;
        }

        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
    }
}
