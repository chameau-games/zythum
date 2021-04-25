using System.Collections.Generic;
using Mirror;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vivox;

namespace Multi
{
    public class CustomNetworkManager : NetworkManager
    {
        //SERV 
        private NetworkConnection _hostConn;
        private NetworkConnection _hostedConn;
        private PlayerManager _host;
        private PlayerManager _hosted;
        private List<NetworkConnection> _initialisedPlayers;
        
        //CLIENT
        public bool isHost => mode == NetworkManagerMode.Host;
        public bool isHosted => !isHost;
        public new static CustomNetworkManager singleton => (CustomNetworkManager) NetworkManager.singleton;

        // I AM A FUCKING SERVER

        public override void OnServerConnect(NetworkConnection conn)
        {
            base.OnServerConnect(conn);
            if (mode == NetworkManagerMode.Host && numPlayers > 0 && networkSceneName.Contains("Lobby"))
            {
                GameObject.Find("StartGameButton").GetComponent<Button>().interactable = true;
            }
        }
        
        public override void OnServerDisconnect(NetworkConnection conn)
        {
            base.OnServerDisconnect(conn);
            _initialisedPlayers.Remove(conn);
            if (mode == NetworkManagerMode.Host && numPlayers < 2 && networkSceneName.Contains("Lobby"))
            {
                GameObject.Find("StartGameButton").GetComponent<Button>().interactable = false;
            }
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            _initialisedPlayers = new List<NetworkConnection>();
            GameManager.singleton.vivoxChannelName = SystemInfo.deviceUniqueIdentifier;
        }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            base.OnServerAddPlayer(conn);
            if (!_initialisedPlayers.Contains(conn))
            {
                _initialisedPlayers.Add(conn);
                GameManager.singleton.TargetAssignLocalPlayer(conn, conn.identity.gameObject);
                GameManager.singleton.TargetConnectToVivox(conn);
            }
        }

        // I AM KIND CLIENT
        public override void OnStopClient()
        {
            base.OnStopClient();
            VivoxManager vivox = GameObject.Find("VivoxManager").GetComponent<VivoxManager>();
            vivox.LeaveChannel();
            vivox.Logout();
            SceneManager.LoadScene("Lobby");
            GameManager.singleton.DeactivatePlayer();
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
            if (mode == NetworkManagerMode.ClientOnly)
            {
                GameObject.Find("WaitMessage").GetComponent<TMP_Text>().text =
                    "En attente du lancement de la partie...";
            }
        }

    }
    
}