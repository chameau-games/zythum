using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Player;
using UnityEngine;

public class ServerTask : MonoBehaviourPun
{

    public GameObject playerCam;
    public PlayerMovement playerMov;

    public bool onCamServ = false;
    
    public void OnClickRack()
    {
        gameObject.SetActive(false);
        //optionsMenu.SetActive(true);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onCamServ && Input.GetKey(KeyCode.E))
        {
            playerCam.SetActive(true);
            playerMov.enabled = true;
        }
    }
    
}
