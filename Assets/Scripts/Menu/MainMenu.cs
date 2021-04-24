using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {

        public GameObject optionsMenu;

        public void OnClickPlayButton()
        {
            SceneManager.LoadScene("Lobby");
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
}
