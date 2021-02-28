using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMenu : MonoBehaviour
{
    
    public AudioMixer audioMixer;
    public GameObject optionMenu;
    
    public void OnChangeGlobalVolume(float volume)
    {
        audioMixer.SetFloat("volMaster", volume);
    }
    
    public void OnChangeAmbientVolume(float volume)
    {
        audioMixer.SetFloat("volAmbient", volume);
    }
    
    public void OnChangeMusicVolume(float volume)
    {
        audioMixer.SetFloat("volMusic", volume);
    }
    
    public void OnChangeVocalVolume(float volume)
    {
        audioMixer.SetFloat("volVocal", volume);
    }
    
    // Return to options menu

    public void OnClickReturnButton()
    {
        gameObject.SetActive(false);
        optionMenu.SetActive(true);
    }
}
