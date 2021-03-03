using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviour
{
    private ScenesManager _sceneManager;
    private CustomNetworkManager _networkManager;

    public GameObject addressInputText;
    public GameObject startGameButton;
    public GameObject hostGameButton;
    public GameObject joinGameButton;
    public GameObject joinSection;
    public GameObject hostSection;
    public GameObject waitMessage;
    public void Start()
    {
        _sceneManager = GameObject.Find("ScenesManager").GetComponent<ScenesManager>();
        _networkManager = (CustomNetworkManager) NetworkManager.singleton;
        _networkManager.networkAddress = "localhost";
    }

    public void JoinGame()
    {
        _networkManager.StopAllConnections();

        _networkManager.StartClient();
        hostSection.SetActive(false);
        joinGameButton.SetActive(false);
        addressInputText.SetActive(false);
        waitMessage.SetActive(true);
    }

    public void HostGame()
    {
        _networkManager.StopAllConnections();

        _networkManager.StartHost(); 
        hostGameButton.SetActive(false);
        startGameButton.SetActive(true);
        joinSection.SetActive(false);
    }

    public void EnableStartButton()
    {
        startGameButton.GetComponent<Button>().interactable = true;
    }

    public void DisableStartButton()
    {
        startGameButton.GetComponent<Button>().interactable = false;
    }

    public void OnClickStartButton()
    {
        _networkManager.StartGame();
    }

    public void Back()
    {
        if (hostSection.activeSelf && joinSection.activeSelf)
        {
            _sceneManager.SwitchScene("MainMenu");
        }
        else
        {
            _networkManager.StopAllConnections();
            if (joinSection.activeSelf)
            {
                hostSection.SetActive(true);
                joinGameButton.SetActive(true);
                addressInputText.SetActive(true);
                waitMessage.SetActive(false);
            }
            else
            {
                hostGameButton.SetActive(true);
                startGameButton.SetActive(false);
                joinSection.SetActive(true);
                startGameButton.GetComponent<Button>().interactable = false;
            }
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
