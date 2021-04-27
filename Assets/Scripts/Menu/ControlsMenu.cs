using UnityEngine;

namespace Menu
{
    public class ControlsMenu : MonoBehaviour
    {
        public GameObject optionMenu;
   
    
        // Return to options menu

        public void OnClickReturnButton()
        {
            gameObject.SetActive(false);
            optionMenu.SetActive(true);
        }
    }
}
