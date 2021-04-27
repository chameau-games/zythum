using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {

        public static GameObject localPlayerInstance;

        private void Awake()
        {
            if (photonView.IsMine)
            {
                PlayerManager.localPlayerInstance = this.gameObject;
                GetComponent<PlayerMovement>().enabled = true;
                transform.Find("Camera").gameObject.SetActive(true);
            }
        
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
