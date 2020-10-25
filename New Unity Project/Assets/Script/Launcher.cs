using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject connectedScreen;
    public GameObject disconnectedScreen;
    public void OnClick_ConnectBtn()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        connectedScreen.SetActive(true);
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        disconnectedScreen.SetActive(true);
    }

    public override void OnJoinedLobby()
    {
        if (disconnectedScreen.activeSelf)
            disconnectedScreen.SetActive(false);
        connectedScreen.SetActive(true);
    }

}
