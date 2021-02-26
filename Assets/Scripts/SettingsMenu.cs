
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audiomixer;

    public GameObject AudioMenu;

    public GameObject VideoMenu;

    public GameObject ControlsMenu;

    public GameObject GameMenu;
    public void Start()
    {
        Screen.fullScreen = true;
    }
    
    // AUDIO
    
    public void OpeningAudioMenu()
    {
        AudioMenu.SetActive(true);
    }
    
    public void ClosingAudioMenu()
    {
        AudioMenu.SetActive(false);
    }
    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("Volume", volume);
    }
    
    // VIDEO
    
    public void OpeningVideoMenu()
    {
        VideoMenu.SetActive(true);
    }
    
    public void ClosingVideoMenu()
    {
        VideoMenu.SetActive(false);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    // CONTROLS

    public void OpeningControlsMenu()
    {
        ControlsMenu.SetActive(true);
    }
    
    public void ClosingControlsMenu()
    {
        ControlsMenu.SetActive(false);
    }
    
    // GAME

    public void OpeningGameMenu()
    {
        GameMenu.SetActive(true);
    }
    
    public void CloingGameMenu()
    {
        GameMenu.SetActive(false);
    }
    

    
}
