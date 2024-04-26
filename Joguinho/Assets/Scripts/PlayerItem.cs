using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public Text playerName;
    public Image backgroundImage;
    public Color highlightColor;
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Image playerAvatar;
    public Sprite[] avatars;

    Player player;


    public void Awake()
    {
        backgroundImage = GetComponent<Image>();
    }

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
        player = _player;
        UpdatePlayerItem(player);

    }

    public void ApplyLocalChanges()
    {
        if (backgroundImage != null)
        {
            backgroundImage.color = highlightColor;
        }
        else
        {
            Debug.LogError("backgroundImage is null in ApplyLocalChanges() method.");
        }

        if (rightArrowButton != null)
        {
            rightArrowButton.SetActive(true);
        }
        else
        {
            Debug.LogError("rightArrowButton is null in ApplyLocalChanges() method.");
        }

        if (leftArrowButton != null)
        {
            leftArrowButton.SetActive(true);
        }
        else
        {
            Debug.LogError("leftArrowButton is null in ApplyLocalChanges() method.");
        }
    }

    public void OnClickLeftArrow()
    {
        if ((int)playerProperties["playerAvatar"] == 0)
        {
            playerProperties["playerAvatar"] = avatars.Length - 1;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickRightArrow()
    {
        if ((int)playerProperties["playerAvatar"] == avatars.Length - 1)
        {
            playerProperties["playerAvatar"] = 0;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
    }

    void UpdatePlayerItem(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
        }
        else
        {
            playerProperties["playerAvatar"] = 0;
        }
    }
    public override void OnJoinedRoom()
    {
        // Verificar se o jogador local é o master client
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.LocalPlayer != null)
        {
            // Definir a propriedade "playerAvatar" para o jogador local
            int initialAvatarIndex = Random.Range(0, avatars.Length); // Índice do avatar inicial
            playerProperties["playerAvatar"] = initialAvatarIndex;
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        }
    }
}
