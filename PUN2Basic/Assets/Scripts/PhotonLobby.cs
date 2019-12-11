using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;

    //Button that makes the player search for a room
    public GameObject battleButton;
    public GameObject cancelButton;

    private void Awake()
    {
        lobby = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Connects to Master photon server
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player jas connected to the Photon master server");
        //All the clients will load to the same scene as the master
        PhotonNetwork.AutomaticallySyncScene = true;
        battleButton.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Faied to join a Random Room, No Rooms available");
        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create Room, Room already existe");
        CreateRoom();
    }


    private void CreateRoom()
    {
        Debug.Log("Room was created");
        int randomRoomName = Random.Range(0, 100);
        RoomOptions roomOptions = new RoomOptions() {IsVisible = true, IsOpen = true, MaxPlayers = 10};
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOptions);

    }

    public void OnBattlerButtonClicked()
    {
        Debug.Log("Batlle button clicked");
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnCancelButtonClicked()
    {
        Debug.Log("Cancel button clicked");
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
