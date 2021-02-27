using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject settingsWindow;
    private ScenesManager _sceneManager;

    private void Start()
    {
        _sceneManager = GameObject.Find("ScenesManager").GetComponent<ScenesManager>();
    }

    public void StartGame()
    {
        _sceneManager.SwitchScene("Lobby");
    }
    
    public void SettingsMenu()
    {
        settingsWindow.SetActive(true);
    }
    
    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
