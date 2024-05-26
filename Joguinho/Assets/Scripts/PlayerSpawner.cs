using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;
    public CinemachineTargetGroup targetGroup; // Referência ao Cinemachine Target Group


    private void Start()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumber];

        if (PhotonNetwork.LocalPlayer == null)
        {
            Debug.LogError("Não conectado à rede Photon.");
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
        GameObject playerInstance = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);

        // Adicione o transformador do jogador instanciado ao Cinemachine Target Group
        targetGroup.AddMember(playerInstance.transform, 1f, 1f);
    }
}
