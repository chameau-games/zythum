using Mirror;
using Multi;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Host : NetworkBehaviour
    {
        private GameObject _startGameButton;

        public override void OnStartClient()
        {
            base.OnStartClient();
            if(isLocalPlayer && CustomNetworkManager.singleton.isHost)
                _startGameButton = GameObject.Find("StartGameButton");
        }

        [TargetRpc]
        public void TargetEnableStartButton(NetworkConnection target)
        {
            if(_startGameButton != null)
                _startGameButton.GetComponent<Button>().interactable = true;
        }

        [TargetRpc]
        public void TargetDisableStartButton(NetworkConnection target)
        {
            if(_startGameButton != null)
                _startGameButton.GetComponent<Button>().interactable = false;
        }
        
        [Command]
        public void CmdStartGame()
        {
            CustomNetworkManager.singleton.ServerChangeScene("Underground");
        }
    }
}