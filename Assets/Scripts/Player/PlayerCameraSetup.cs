using System;
using Mirror;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class PlayerCameraSetup : NetworkBehaviour
{
    private GameObject _mainCamera;
    public PlayerMovement playerMovement;
    private bool _isPlayerCameraEnabled = false;

    private void Update()
    {
        if (isLocalPlayer && _isPlayerCameraEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ChangeCursorState(false);
                playerMovement.isGamePaused = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                ChangeCursorState(true);
                playerMovement.isGamePaused = false;
            }
        }
    }

    private void OnEnable()
    {
        _mainCamera = Camera.main.gameObject;
    }

    private void OnDisable()
    {
        if (isLocalPlayer)
        {
            //Enable main camera and disable player camera
            SwitchCamera(CameraType.Global);

            _isPlayerCameraEnabled = false;
        }
    }

    public void InitCamera()
    {
        //Disable main camera and enable player camera
        SwitchCamera(CameraType.Player);

        //Config the cursor
        ChangeCursorState(true);

        _isPlayerCameraEnabled = true;
    }

    private enum CameraType
    {
        Player,
        Global
    }

    private void SwitchCamera(CameraType cameraType)
    {
        bool activatePlayerCam = cameraType == CameraType.Player;
        if (_mainCamera != null) _mainCamera.SetActive(!activatePlayerCam);
        gameObject.transform.Find("Camera").gameObject.SetActive(activatePlayerCam);
    }

    public void ChangeCursorState(bool locked)
    {
        Cursor.visible = !locked;
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
    }
}