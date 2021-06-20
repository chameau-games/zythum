using System;
using Menu;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerActions : MonoBehaviour
    {
        public float maxReach;
        private HUD hud;
        private Transform spawnpointSdc;

        private Transform aerationVent;
        private Transform electricalPanel;

        private void Start()
        {
            Debug.Log("a");
            hud = GameObject.Find("HUD").GetComponent<HUD>();
            spawnpointSdc = GameObject.Find("Spawnpoint salle de contrôle").transform;
            aerationVent = GameObject.Find("aerationVent").transform;
            electricalPanel = GameObject.Find("tableau électrique").transform;
        }

        // Update is called once per frame
        void Update()
        {
            hud.HideText();
            RaycastHit hit;
            if (Camera.current != null && 
                Physics.Raycast(Camera.current.transform.position, Camera.current.transform.forward, out hit,
                maxReach, LayerMask.GetMask("Map")))
            {
                //ça touche la bouche d'aération (pour passer dans la salle de controle & c'est le joueur qui peut y aller
                if (PhotonNetwork.IsMasterClient && hit.transform == aerationVent)
                {
                    if (Input.GetKey(KeyCode.U))
                    {
                        transform.SetPositionAndRotation(spawnpointSdc.position, spawnpointSdc.rotation);
                    }
                    else
                    {
                        hud.SetInformationText("Appuie sur U pour passer dans la bouche d'aération");
                        hud.ShowText();
                    }
                }

                if (!PhotonNetwork.IsMasterClient && hit.transform == electricalPanel)
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        var task = GameObject.Find("salle electrique").GetComponent<ElectricalTask>();
                        task.playerCamera = Camera.current.gameObject;
                        Camera.current.gameObject.SetActive(false);
                        task.taskCamera.gameObject.SetActive(true);
                    }
                    else
                    {
                        hud.SetInformationText("Appuie sur E pour modifier le panneau électrique");
                        hud.ShowText();
                    }
                }
            }
        }
    }
}
