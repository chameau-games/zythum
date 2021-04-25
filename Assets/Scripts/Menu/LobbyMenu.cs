using Mirror;
using Multi;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class LobbyMenu : MonoBehaviour
    {
        private CustomNetworkManager _networkManager;

        public GameObject addressInputText;
        public GameObject startGameButton;
        public GameObject hostGameButton;
        public GameObject joinGameButton;
        public GameObject joinSection;
        public GameObject hostSection;
        public GameObject waitMessage;

        private void Start()
        {
            _networkManager = CustomNetworkManager.singleton;
        }

        public void OnClickJoinButton()
        {
            hostSection.SetActive(false);
            joinGameButton.SetActive(false);
            addressInputText.SetActive(false);
            waitMessage.SetActive(true);
            _networkManager.StartClient();
        }

        public void OnClickHostButton()
        {
            hostGameButton.SetActive(false);
            startGameButton.SetActive(true);
            joinSection.SetActive(false);
            
            _networkManager.StartHost();
        }

        public void OnClickStartButton()
        {
            GameManager.singleton.CmdAskForStart();
        }

        public void OnClickBackButton()
        {
            if (hostSection.activeSelf && joinSection.activeSelf)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                Debug.Log(_networkManager.mode.ToString());
                if(_networkManager.mode == NetworkManagerMode.Host)
                    _networkManager.StopHost();
                else if(_networkManager.mode == NetworkManagerMode.ClientOnly)
                    _networkManager.StopClient();
            }

        }

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
            _networkManager.networkAddress = addressInputText.GetComponent<TMP_InputField>().text.Trim();
        }

     
    }
}
