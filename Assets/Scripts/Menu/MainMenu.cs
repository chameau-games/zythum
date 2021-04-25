using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject optionsMenu;
    private ScenesManager _sceneManager;

    private void Start()
    {
        //_sceneManager = GameObject.Find("ScenesManager").GetComponent<ScenesManager>();
    }

    public void OnClickPlayButton()
    {
        _sceneManager.SwitchScene("Lobby");
    }
    
    public void OnClickOptionButton()
    {
        gameObject.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
