using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text roomName;
    LobbyManager manager;

    public void Start()
    {
        manager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomName(string _roomName)
    {
        if (roomName != null)
        {
            roomName.text = _roomName;
        }
        else
        {
            Debug.LogError("Cannot set room name: roomName is null.");
        }
    }

    public void OnClickItem()
    {
        if (roomName != null)
        {
            manager.JoinRoom(roomName.text);
        }
        else
        {
            Debug.LogError("Cannot join room: roomName is null.");
        }
    }

}
