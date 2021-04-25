using System;
using System.Security.Policy;
using Menu;
using Mirror;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        //CLIENT
        public bool isHost => mode == NetworkManagerMode.Host;
        public bool isHosted => !isHost;
        public new static CustomNetworkManager singleton => (CustomNetworkManager) NetworkManager.singleton;

        // I AM A FUCKING SERVER
        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            base.OnServerAddPlayer(conn);
            Debug.Log("aaaa");
            if (numPlayers == 1 && _host == null)
            {
                _hostConn = conn;
                _host = conn.identity.gameObject.GetComponent<PlayerManager>();
                
                //VIVOX
                _host.channelName = SystemInfo.deviceUniqueIdentifier;
            }
            else if (numPlayers == 2 && _hosted == null)
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
            if (numPlayers < 2 && conn != _hostConn)
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
            Debug.Log("heyyy this is not very nice");
            if (networkSceneName != "Underground")
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                if(NetworkClient.connection.identity != null)
                    NetworkClient.connection.identity.GetComponent<PlayerManager>().Activate();
            }
        }

        public override void OnStopClient()
        {
            base.OnStopClient();
            SceneManager.LoadScene("Lobby");
            VivoxManager vivox = GameObject.Find("VivoxManager").GetComponent<VivoxManager>();
            vivox.LeaveChannel();
            vivox.Logout();
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
            VivoxManager vivox = GameObject.Find("VivoxManager").GetComponent<VivoxManager>();
            vivox.Login();
        }
    }
}