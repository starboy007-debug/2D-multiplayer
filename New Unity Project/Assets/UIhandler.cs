using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class UIhandler : MonoBehaviourPunCallbacks
{
    public InputField createRoomTF;
    public InputField joinRoomTF;

    public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomTF.text,null);
    }

    public void OnClick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomTF.text,new RoomOptions {MaxPlayers = 4},null);
    }

    public override void OnJoinedRoom()
    {
        print("Room joined sucessfully");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Room joined Failed "+returnCode+" message "+message);

    }

}
