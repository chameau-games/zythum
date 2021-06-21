using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Menu;
using Photon.Pun;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Player
{
    public class PlayerActions : MonoBehaviour
    {
        public float maxReach;
        private HUD hud;
        private GameObject hudCanvas;
        private Transform spawnpointSdc;

        private Transform aerationVent;
        private Transform electricalPanel;
        private Transform hudMissionList;

        public GameObject playerCamera;
        public PlayerMovement playerMovement;

        private void Start()
        {
            hudCanvas = GameObject.Find("HUD");
            hud = hudCanvas.GetComponent<HUD>();
            spawnpointSdc = GameObject.Find("Spawnpoint salle de contrôle").transform;
            //aerationVent = GameObject.Find("aerationVent").transform;
            electricalPanel = GameObject.Find("tableau électrique").transform;
            hudMissionList = GameObject.Find("MissionListCanvas").transform;
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
                /*if (PhotonNetwork.IsMasterClient && hit.transform == aerationVent)
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
                }*/
                
                if (!PhotonNetwork.IsMasterClient && hit.transform == electricalPanel)
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        ElectricalTask task = GameObject.Find("ElectricalTask").GetComponent<ElectricalTask>();
                        Debug.Log(GameObject.Find("ElectricalTask").name);
                        task.SetPlayerCamera(playerCamera);
                        task.SetPlayerMovement(playerMovement);
                        task.taskHUD.gameObject.SetActive(true);
                        task.taskCamera.gameObject.SetActive(true);
                        task.taskCamera.enabled = true;
                        hudCanvas.gameObject.SetActive(false);
                        playerCamera.SetActive(false);
                        playerMovement.enabled = false;

                    }
                    else
                    {
                        hud.SetInformationText("Appuie sur E pour modifier le panneau électrique");
                        hud.ShowText();
                    }
                }
                
                if (PhotonNetwork.IsMasterClient && hit.transform == hudMissionList)
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        var task = GameObject.Find("MissionList").GetComponent<MissionList>();
                        task.playerCamera = Camera.current.gameObject;
                        Camera.current.gameObject.SetActive(false);
                        task.taskCamera.gameObject.SetActive(true);
                        hudCanvas.gameObject.SetActive(false);
                        task.taskHUD.gameObject.SetActive(true);
                    }
                    else
                    {
                        hud.SetInformationText("Appuie sur E pour consulter la liste des missions");
                        hud.ShowText();
                    }
                }
            }
        }
    }
}
