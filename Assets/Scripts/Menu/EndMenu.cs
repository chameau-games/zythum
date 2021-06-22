using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class EndMenu : MonoBehaviourPunCallbacks
    {
    
        public Camera winCamera;
        public Camera gameoverCamera;
        private GameObject[] _players;
        // Start is called before the first frame update
        void Start()
        {
            bool hasWin = (bool) PhotonNetwork.CurrentRoom.CustomProperties["hasWin"];
            _players=GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject p in _players)
            {
                p.transform.Find("Camera").gameObject.SetActive(false);
            }
            if (!hasWin)
            {
                gameoverCamera.gameObject.SetActive(true);
                winCamera.gameObject.SetActive(false);
            }
            else
            {
                gameoverCamera.gameObject.SetActive(false);
                winCamera.gameObject.SetActive(true);
            }

            StartCoroutine(backtomenu());
        }

        IEnumerator backtomenu()
        {
            yield return new WaitForSeconds(5);
            PhotonNetwork.Disconnect();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
