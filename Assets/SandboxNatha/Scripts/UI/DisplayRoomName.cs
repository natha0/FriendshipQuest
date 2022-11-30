using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayRoomName : MonoBehaviour
{

    public GameObject roomNamePanel;
    private TMP_Text roomNameText;

    void Start()
    {
        roomNameText = roomNamePanel.transform.Find("Room Name Text").GetComponent<TMP_Text>();
        RoomProperties[] roomList = GameObject.FindObjectsOfType<RoomProperties>();
        foreach(RoomProperties room in roomList)
        {
            room.setRoomName += SetRoomName;
        }
    }


    void SetRoomName(string roomName)
    {
        roomNameText.text = roomName;
    }
}
