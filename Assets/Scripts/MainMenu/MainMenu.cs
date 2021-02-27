using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject optionsMenu;
    private ScenesManager _sceneManager;

    private void Start()
    {
        _sceneManager = GameObject.Find("ScenesManager").GetComponent<ScenesManager>();
    }

    public void StartGame()
    {
        _sceneManager.SwitchScene("Lobby");
    }
    
    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }
    
    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
