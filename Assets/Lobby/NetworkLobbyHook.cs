using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;


public class NetworkLobbyHook : LobbyHook {

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer,GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        ArmRotation localPlayer = gamePlayer.GetComponent<ArmRotation>();
       

        localPlayer.pName = lobby.playerName;
        localPlayer.PlayerColor = lobby.playerColor;

        Debug.Log("Player Color"+localPlayer.PlayerColor);

    }
    // Use this for initialization

}
