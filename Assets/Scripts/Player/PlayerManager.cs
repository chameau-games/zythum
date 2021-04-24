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

        public void Activate()
        {
            transform.Find("Camera").gameObject.SetActive(true);
            GetComponent<PlayerMovement>().enabled = true;
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            if (CustomNetworkManager.singleton.isHost)
            {
                _startGameButton = GameObject.Find("StartGameButton");
            }
            else if (CustomNetworkManager.singleton.isHosted)
            {
                _waitMessage = GameObject.Find("WaitMessage")?.GetComponent<TMP_Text>();
            }
        }

        [TargetRpc]
        public void TargetEnableStartButton(NetworkConnection target)
        {
            if (_startGameButton != null)
                _startGameButton.GetComponent<Button>().interactable = true;
        }

        [TargetRpc]
        public void TargetDisableStartButton(NetworkConnection target)
        {
            if (_startGameButton != null)
                _startGameButton.GetComponent<Button>().interactable = false;
        }

        [TargetRpc]
        public void TargetSetWaitMessage(NetworkConnection conn, string msg)
        {
            if (_waitMessage != null)
                _waitMessage.text = msg;
        }

        [Command]
        public void CmdStartGame()
        {
            CustomNetworkManager.singleton.ServerChangeScene("Underground");
        }
    }
}