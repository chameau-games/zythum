using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public GameObject optionMenu;
   
    
    // Return to options menu

    public void OnClickReturnButton()
    {
        gameObject.SetActive(false);
        optionMenu.SetActive(true);
    }
}
