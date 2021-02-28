using Mirror;

public class PlayerManager : NetworkBehaviour
{
    private PlayerCameraSetup _playerCameraSetup;
    private PlayerMovement _playerMovement;

    private void Init()
    {
        _playerCameraSetup = GetComponent<PlayerCameraSetup>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void CheckIfLocalAndActivate()
    {
        if (isLocalPlayer)
        {
            Init();
            
            _playerCameraSetup.Init();
            _playerMovement.Init();
            
            _playerCameraSetup.enabled = true;
            _playerMovement.enabled = true;
            
            _playerCameraSetup.InitCamera();
        }
    }
    
}