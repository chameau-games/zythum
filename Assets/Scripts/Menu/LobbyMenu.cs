using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vivox;

namespace Menu
{
    public class LobbyMenu : MonoBehaviourPunCallbacks
    {

        public GameObject addressInputText;
        public GameObject startGameButton;
        public GameObject hostGameButton;
        public GameObject joinGameButton;
        public GameObject backButton;
        public GameObject joinSection;
        public GameObject hostSection;
        public GameObject waitMessage;
        public GameObject hostRoomCode;
        public GameObject connectionText;
        public string gameVersion;

        private string _roomCode;
        private GameObject _offlineP1;
        private GameObject _offlineP2;

        [SerializeField]
        public List<GameObject> spawnpoints;

        private void Start()
        {
            ConnectToPun();
        }

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        
        private void ConnectToPun()
        {
            if (!PhotonNetwork.IsConnected){
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        private void CreateRandomRoom()
        {
            string randomString = new System.Random().Next(10000, 99999).ToString();
            PhotonNetwork.CreateRoom(randomString, new RoomOptions {MaxPlayers = 2});
        }
        
        #region PunCallbacks

        public override void OnDisconnected(DisconnectCause cause)
        {
            SceneManager.LoadScene("MainMenu");
        }

        public override void OnConnectedToMaster()
        {
            hostSection.SetActive(true);
            joinSection.SetActive(true);
            backButton.SetActive(true);
            connectionText.SetActive(false);
        }

        public override void OnLeftRoom()
        {
            GameObject.Find("VivoxManager").GetComponent<VivoxManager>().LeaveChannel();
            SceneManager.LoadScene("Lobby");
        }

        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                hostGameButton.SetActive(false);
                startGameButton.SetActive(true);
                hostRoomCode.SetActive(true);
                joinSection.SetActive(false);
                UpdateHostRoomCode(PhotonNetwork.CurrentRoom.Name);
                _offlineP1 = PhotonNetwork.InstantiateRoomObject("OfflinePlayer",
                    spawnpoints[0].transform.position,
                    spawnpoints[0].transform.rotation);
            }
            else
            {
                hostSection.SetActive(false);
                joinGameButton.SetActive(false);
                addressInputText.SetActive(false);
                waitMessage.SetActive(true);
                waitMessage.GetComponent<TMP_Text>().text = "En attente du lancement de la partie";
            }
            GameObject.Find("VivoxManager").GetComponent<VivoxManager>().Login();
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                UpdateStartButton();
                _offlineP2 = PhotonNetwork.InstantiateRoomObject("OfflinePlayer",
                    spawnpoints[1].transform.position,
                    spawnpoints[1].transform.rotation);
            }

        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                UpdateStartButton();
                spawnpoints[0].SetActive(true);
                PhotonNetwork.Destroy(_offlineP2);
            }
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            CreateRandomRoom();
        }

        #endregion

        #region JoinHostBackButtons

        public void OnClickJoinButton()
        {
            PhotonNetwork.JoinRoom(_roomCode);
        }

        public void OnClickHostButton()
        {
            CreateRandomRoom();
        }

        public void OnClickBackButton()
        {
            if (!PhotonNetwork.InRoom)
            {
                PhotonNetwork.Disconnect();
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                if(PhotonNetwork.IsMasterClient)
                    PhotonNetwork.Destroy(_offlineP1);
                PhotonNetwork.LeaveRoom();
            }
        }

        #endregion

        #region StartButton

        public void OnClickStartButton()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Underground");
            }
        }
        private void UpdateStartButton()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                startGameButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                startGameButton.GetComponent<Button>().interactable = true;
            }
        }

        #endregion

        private void UpdateHostRoomCode(string code)
        {
            hostRoomCode.GetComponent<TMP_Text>().text = "Invitez votre ami à l'aide du code : " + code;
        }
        
        #region RoomCodeInputText

        public void OnSelectInputText()
        {
            addressInputText.transform.Find("Text Area/Placeholder").gameObject.SetActive(false);
        }
    
        public void OnDeselectInputText()
        {
            addressInputText.transform.Find("Text Area/Placeholder").gameObject.SetActive(true);
        }

        public void OnEditInputText()
        {
            _roomCode = addressInputText.GetComponent<TMP_InputField>().text.Trim();
        }

        #endregion
        
        
    }
}
