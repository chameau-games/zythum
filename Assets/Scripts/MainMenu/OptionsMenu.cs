
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public AudioMixer audiomixer;

    public GameObject AudioMenu;

    public GameObject VideoMenu;

    public GameObject ControlsMenu;

    public GameObject GameMenu;
    public void Start()
    {
        
    }
    
    // AUDIO
    
    public void OpenAudioMenu()
    {
        AudioMenu.SetActive(true);
    }
    
    public void CloseAudioMenu()
    {
        AudioMenu.SetActive(false);
    }
    
    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("Volume", volume);
    }
    
    // VIDEO
    
    public void OpenVideoMenu()
    {
        VideoMenu.SetActive(true);
    }
    
    public void CloseVideoMenu()
    {
        VideoMenu.SetActive(false);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    // CONTROLS

    public void OpenControlsMenu()
    {
        ControlsMenu.SetActive(true);
    }
    
    public void CloseControlsMenu()
    {
        ControlsMenu.SetActive(false);
    }
    
    // GAME

    public void OpenGameMenu()
    {
        GameMenu.SetActive(true);
    }
    
    public void CloseGameMenu()
    {
        GameMenu.SetActive(false);
    }
    

    
}
