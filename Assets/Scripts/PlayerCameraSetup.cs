using System;
using Mirror;
using UnityEngine;

public class PlayerCameraSetup : NetworkBehaviour
{
    private Camera _mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        if (isLocalPlayer)
        {
            if (_mainCamera != null) _mainCamera.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    private void OnDisable()
    {
        if (_mainCamera != null) _mainCamera.gameObject.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
