using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class MissionManual : MonoBehaviour
{
    public GameObject taskCamera;
    public GameObject playerCamera;
    public PlayerMovement playerMovement;
    public GameObject hud;

    public Image image;
    public Sprite[] images;

    private int index;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            taskCamera.SetActive(false);
            playerCamera.SetActive(true);
            playerMovement.enabled = true;
            hud.SetActive(true);
        }
    }
    
    public void OnNextButtonClick()
    {
        Debug.Log(index);
        Debug.Log("ababa");
        index = (index + 1) % images.Length;
        image.GetComponent<Image>().sprite = images[index];
        Debug.Log(index);
    }
    
    public void OnBackButtonClick()
    {
        index = (index - 1 + images.Length) % images.Length;
        image.GetComponent<Image>().sprite = images[index];
    }

}
