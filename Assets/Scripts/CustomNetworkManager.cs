using Mirror;
using Player;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    private ScenesManager _sceneManager;

    public struct InitGameMessage : NetworkMessage
    {
    };

    public override void Start()
    {
        base.Start();
        _sceneManager = GameObject.Find("ScenesManager").GetComponent<ScenesManager>();
        NetworkClient.RegisterHandler<InitGameMessage>(ReceiveInitGameMessage);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
        if (numPlayers >= 2)
        {
            GameObject.Find("LobbyCanvas").GetComponent<LobbyMenu>().EnableStartButton();
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);

        if (numPlayers < 2)
        {
            GameObject.Find("LobbyCanvas").GetComponent<LobbyMenu>().DisableStartButton();
        }
    }

    public void StartGame()
    {
        NetworkServer.SendToAll(new InitGameMessage() { });
    }

    public void ReceiveInitGameMessage(NetworkConnection conn, InitGameMessage msg)
    {
        _sceneManager.SwitchScene("Underground");
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            _sceneManager.Move(player, "Underground");
            player.GetComponent<PlayerManager>().CheckIfLocalAndActivate();
        }
    }
    
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        _sceneManager.SwitchScene("Lobby");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StopAllConnections()
    {
        if (mode == NetworkManagerMode.Host)
        {
            StopHost();
        }
        else if(mode == NetworkManagerMode.ClientOnly)
        {
            StopClient();
        }
    }
}