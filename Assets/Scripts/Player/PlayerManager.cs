using Mirror;

namespace Player
{
    public class PlayerManager : NetworkBehaviour
    {
        public PlayerCameraSetup playerCameraSetup;
        public PlayerMovement playerMovement;
    
        public void CheckIfLocalAndActivate()
        {
            if (isLocalPlayer)
            {
                playerCameraSetup.enabled = true;
                playerMovement.enabled = true;
            
                playerCameraSetup.InitCamera();
            }
        }
    
    }
}