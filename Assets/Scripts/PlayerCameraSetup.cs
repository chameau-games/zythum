using Mirror;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class PlayerCameraSetup : NetworkBehaviour
{
    private Camera _mainCamera;
    private PlayerMovement _playerMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _playerMovement = GetComponentInParent<PlayerMovement>();
        
        if (isLocalPlayer)
        {
            if (_mainCamera != null)
                _mainCamera.gameObject.SetActive(false);
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

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                _playerMovement.isGamePaused = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                _playerMovement.isGamePaused = false;
            }
        }
    }
    
    private void OnDisable()
    {
        if (isLocalPlayer)
        {
            if (_mainCamera != null)
                _mainCamera.gameObject.SetActive(true);
        }
    }
}
