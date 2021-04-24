using Mirror;
using Multi;
using TMPro;
using UnityEngine;

namespace Player
{
    public class Hosted : NetworkBehaviour
    {
        private TMP_Text _waitMessage;
        
        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            if (isLocalPlayer && CustomNetworkManager.singleton.isHosted 
                              && GameObject.Find("WaitMessage") != null)
                _waitMessage = GameObject.Find("WaitMessage").GetComponent<TMP_Text>();
        }

        [TargetRpc]
        public void TargetSetWaitMessage(NetworkConnection conn, string msg)
        {
            if(_waitMessage != null)
                _waitMessage.text = msg;
        }
        
        
    }
}