using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Menu
{
    public class VideoMenu : MonoBehaviour
    {
        public GameObject optionMenu;

        public TMP_Dropdown resolutionDropdown;
    
        Resolution[] _resolutions;

        public void OnEnable()
        {
            _resolutions = Screen.resolutions; //.Select(resolution => new Resolution { width = resolution.width, height = resolution.height}).Distinct().ToArray();
            resolutionDropdown.ClearOptions();
            Debug.Log(_resolutions.Length);

            List<string> resolutionsOptions = new List<string>();
            int index = 0;
            for (int i = 0; i < _resolutions.Length; i++)
            {
                string option = _resolutions[i].width + "x" + _resolutions[i].height;
                resolutionsOptions.Add(option);
                if (_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
                    index = i;
            }

            resolutionDropdown.AddOptions(resolutionsOptions);
            // SetResolution(index);
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
            Resolution resolution = _resolutions[index];
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
}
