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
        index = (index + 1) % images.Length;
        image.GetComponent<Image>().sprite = images[index];
    }
    
    public void OnBackButtonClick()
    {
        index = (index - 1 + images.Length) % images.Length;
        image.GetComponent<Image>().sprite = images[index];
    }

}
