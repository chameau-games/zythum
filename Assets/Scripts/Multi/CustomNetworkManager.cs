using System;
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
        private Host _host;
        private Hosted _hosted;
        
        //CLIENT
        private GameObject _clientGameObject;
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
                _host = conn.identity.gameObject.GetComponent<Host>();
            }
            else if (numPlayers == 2)
            {
                _hostedConn = conn;
                _hosted = conn.identity.gameObject.GetComponent<Hosted>();
                
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
                _clientGameObject = NetworkClient.connection.identity.gameObject;
                _clientGameObject.transform.Find("Camera").gameObject.SetActive(true);
                _clientGameObject.GetComponent<PlayerMovement>().enabled = true;
            }
        }

        public override void OnStopClient()
        {
            base.OnStopClient();
            SceneManager.LoadScene("Lobby");
        }
    }
}