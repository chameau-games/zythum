using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    private List<GameObject> _players = new List<GameObject>();
    private ScenesManager _sceneManager;

    public struct InitGameMessage : NetworkMessage
    {
        public List<GameObject> players;
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
        
        _players.Add(conn.identity.gameObject);
        if (numPlayers == 2)
        {
            NetworkServer.SendToAll(new InitGameMessage() {players = _players});
        }
    }

    public void ReceiveInitGameMessage(NetworkConnection conn, InitGameMessage msg)
    {
        _sceneManager.SwitchScene("Underground");
        foreach (GameObject player in msg.players)
        {
            _sceneManager.Move(player, "Underground");
            player.GetComponent<PlayerManager>().CheckIfLocalAndActivate();
        }
    }
}