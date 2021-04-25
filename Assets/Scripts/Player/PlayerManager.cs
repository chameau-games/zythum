using Mirror;
using Multi;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerManager : NetworkBehaviour
    {
        private GameObject _startGameButton;
        private TMP_Text _waitMessage;
        public PlayerMovement playerMovement;
        public GameObject camera;

        [HideInInspector]
        public static GameObject localPlayer;

        public void Activate()
        {
            if (isLocalPlayer)
            {
                camera.SetActive(true);
                playerMovement.enabled = true;
            }
        }
        
        public void Deactivate()
        {
            if (isLocalPlayer)
            {
                camera.SetActive(false);
                playerMovement.enabled = false;
            }
            
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            localPlayer = gameObject;
            if (CustomNetworkManager.singleton.isHost)
            {
                _startGameButton = GameObject.Find("StartGameButton");
            }
            else if (CustomNetworkManager.singleton.isHosted)
            {
                _waitMessage = GameObject.Find("WaitMessage")?.GetComponent<TMP_Text>();
            }
        }
    }
}