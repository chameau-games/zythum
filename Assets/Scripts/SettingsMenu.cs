
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audiomixer;

    Resolution[] resolutions;

    public Dropdown resolutionDropdown;
    public void Start()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();
        
        List<string> options = new List<string>();

        int compteur = 0;
        int currentResolutionIndex = 0;
        foreach (Resolution resolution in resolutions)
        {
            string option = resolution.width + "x" + resolution.height;
            options.Add(option);
            if (resolution.width == Screen.width && resolution.height == Screen.height)
            {
                currentResolutionIndex = compteur;
            }
            compteur++;
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        Screen.fullScreen = true;
    }
    
    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("Volume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
