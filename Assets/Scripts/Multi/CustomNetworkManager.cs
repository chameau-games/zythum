using System;
using System.Security.Policy;
using Menu;
using Mirror;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Multi
{
    public class CustomNetworkManager : NetworkManager
    {
        //SERV 
        private NetworkConnection _hostConn;
        private NetworkConnection _hostedConn;
        private PlayerManager _host;
        private PlayerManager _hosted;

        //CLIENT
        public bool isHost => mode == NetworkManagerMode.Host;
        public bool isHosted => !isHost;
        public new static CustomNetworkManager singleton => (CustomNetworkManager) NetworkManager.singleton;

        // I AM A FUCKING SERVER
        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            base.OnServerAddPlayer(conn);
            if (numPlayers == 1)
            {
                _hostConn = conn;
                _host = conn.identity.gameObject.GetComponent<PlayerManager>();
                
                //VIVOX
                _host.channelName = SystemInfo.deviceUniqueIdentifier;
            }
            else if (numPlayers == 2)
            {
                _hostedConn = conn;
                _hosted = conn.identity.gameObject.GetComponent<PlayerManager>();
                _hosted.channelName = _host.channelName;

                _host.TargetEnableStartButton(_hostConn);
                _hosted.TargetSetWaitMessage(_hostedConn, "En attente du lancement de la partie...");
            }
        }
        

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            base.OnServerDisconnect(conn);
            if (numPlayers < 2)
            {
                _host.TargetDisableStartButton(_hostConn);
            }
        }

        // I AM KIND CLIENT
        public void StartGame()
        {
            _host.CmdStartGame();
        }

        public override void OnClientSceneChanged(NetworkConnection conn)
        {
            base.OnClientSceneChanged(conn);
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Underground"))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                NetworkClient.connection.identity.GetComponent<PlayerManager>().Activate();
            }
        }

        public override void OnStopClient()
        {
            base.OnStopClient();
            SceneManager.LoadScene("Lobby");
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
            var vivox = GameObject.Find("VivoxManager").GetComponent<VivoxManager>();
            vivox.Login();
        }
    }
}