using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private string _current;
    // Start is called before the first frame update
    private void Start()
    {
        _current = "MainMenu";
        SceneManager.LoadScene(_current, LoadSceneMode.Additive);
    }

    public void SwitchScene(string newSceneName)
    {
        SceneManager.UnloadSceneAsync(_current);
        _current = newSceneName;
        SceneManager.LoadScene(_current, LoadSceneMode.Additive);
    }

    public void Move(GameObject go, string sceneName)
    {
        SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName(sceneName));
    }
    
}
