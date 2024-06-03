using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField roomInputField;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public Text roomName;

    public RoomItem roomItemPrefab;
    public Text roomItemT;
    List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;

    public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;

    public GameObject playButton;

    public void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    /*public void OnClickCreate()
    {
        //Cria a sala
        if (roomInputField.text.Length >= 1 && PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions() { MaxPlayers = 3, BroadcastPropsChangeToAll = true });
            //roomItemObj.SetActive(true);
            roomItemT.text = roomInputField.text;


        }
        else
        {
            Debug.LogError("Unable to create room: Photon is not connected and ready.");
        }
    }
    */
    public void OnClickCreate()
    {
        if (roomInputField.text.Length >= 1 && PhotonNetwork.IsConnectedAndReady)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 3;
            roomOptions.BroadcastPropsChangeToAll = true;

            // Adicionar o nome da sala às propriedades customizadas
            ExitGames.Client.Photon.Hashtable roomProperties = new ExitGames.Client.Photon.Hashtable();
            roomProperties.Add("roomName", roomInputField.text);
            roomOptions.CustomRoomProperties = roomProperties;
            roomOptions.CustomRoomPropertiesForLobby = new string[] { "roomName" };

            PhotonNetwork.CreateRoom(roomInputField.text, roomOptions);
        }
        else
        {
            Debug.LogError("Unable to create room: Photon is not connected and ready.");
        }
    }
    public override void OnJoinedRoom()
    {
        // Atualizar o nome da sala usando propriedades customizadas
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("roomName"))
        {
            roomName.text = "Nome da sala: " + PhotonNetwork.CurrentRoom.CustomProperties["roomName"].ToString();
        }
        else
        {
            roomName.text = "Nome da sala: " + PhotonNetwork.CurrentRoom.Name;
        }

        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        UpdatePlayerList();
    }
    /*
        public override void OnJoinedRoom()
        {
            //Entra na sala e troca seu nome
            lobbyPanel.SetActive(false);
            roomPanel.SetActive(true);
            roomName.text = "Nome da sala: " + PhotonNetwork.CurrentRoom.Name;
            UpdatePlayerList();
        }
    */

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            //Fica atualizando a lista.
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }

    }
    /*
    void UpdateRoomList(List<RoomInfo> list)
    {
        //Funcao para atualizar, fica checando se a sala ainda existe, caso
        //nao exista, ela e apagada. E checa se uma nova sala foi criada.
        foreach (RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach (RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }
*/
    void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach (RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);

            string roomName = room.Name;
            if (room.CustomProperties.ContainsKey("roomName"))
            {
                roomName = room.CustomProperties["roomName"].ToString();
            }

            newRoom.SetRoomName(roomName);
            roomItemsList.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnclickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public void OnclickLeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }
    public override void OnLeftLobby()
    {
        PhotonNetwork.LoadLevel("Menu");
    }

    public override void OnConnectedToMaster()
    {

        PhotonNetwork.JoinLobby();
    }

    void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }

        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            }

            playerItemsList.Add(newPlayerItem);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    public void OnClickPlayButton()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel("GameplayMultiplayer");
    }
}
