using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoMenu : MonoBehaviour
{
    public GameObject optionMenu;

    public TMP_Dropdown resolutionDropdown;
    
    Resolution[] resolutions;

    public void Start()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height}).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> resolutionsOptions = new List<string>();
        int index = 0;
        int compteur = 0;
        foreach (var resolution in resolutions)
        {
            string option = resolution.width + "x" + resolution.height;
            resolutionsOptions.Add(option);
            if (resolution.width == Screen.width && resolution.height == Screen.height)
                index = compteur;
            compteur++;
        }
        
        resolutionDropdown.AddOptions(resolutionsOptions);
        resolutionDropdown.value = index;
        resolutionDropdown.RefreshShownValue();
    }
    // Return to options menu

    public void OnClickReturnButton()
    {
        gameObject.SetActive(false);
        optionMenu.SetActive(true);
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }

    public void SetFullScreen(int index)
    {
        if (index == 0)
            Screen.fullScreen = false;
        else
            Screen.fullScreen = true; 
    }
}
