using UnityEngine;

namespace Menu
{
    public class OptionsMenu : MonoBehaviour
    {
    
        public GameObject audioMenu;
        public GameObject videoMenu;
        public GameObject controlsMenu;
        public GameObject gameMenu;
        public GameObject mainMenu;
    
    
        // Go to audio menu

        public void OnClickAudioButton()
        {
            gameObject.SetActive(false);
            audioMenu.SetActive(true);
        }

        // Go to video menu

        public void OnClickVideoButton()
        {
            gameObject.SetActive(false);
            videoMenu.SetActive(true);
        }

        // Go to controls menu

        public void OnClickControlsButton()
        {
            gameObject.SetActive(false);
            controlsMenu.SetActive(true);
        }
    
        // Go to game menu

        public void OnClickGameButton()
        {
            gameObject.SetActive(false);
            gameMenu.SetActive(true);
        }

        // Return to main menu

        public void OnClickReturnButton()
        {
            gameObject.SetActive(false);
            mainMenu.SetActive(true);
        }
    
    }
}
