using Mirror;
using UnityEngine.SceneManagement;

public class ScenesManager : NetworkBehaviour
{
    private string _current;
    // Start is called before the first frame update
    void Start()
    {
        _current = "MainMenu";
        SceneManager.LoadScene(_current, LoadSceneMode.Additive);
    }

    public void SwitchScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(_current);
        _current = name;
    }
    
}
