using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Menu;
using Photon.Pun;
using Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Player
{
    public class PlayerActions : MonoBehaviourPun
    {
        private int code;
        public float maxReach;
        private HUD hud;
        private GameObject hudCanvas;
        private Transform spawnpointSdc;

        private Transform aerationVent;
        private Transform electricalPanel;
        private Transform hudMissionManual;

        private GameManager gameManager;

        public GameObject playerCamera;
        public PlayerMovement playerMovement;
        
        

        private void Start()
        {
            if (!photonView.IsMine)
            {
                enabled = false;
                return;
            }
            hudCanvas = GameObject.Find("HUD");
            hud = hudCanvas.GetComponent<HUD>();
            spawnpointSdc = GameObject.Find("Spawnpoint salle de contrôle").transform;
            aerationVent = GameObject.Find("aerationVent").transform;
            electricalPanel = GameObject.Find("tableau électrique").transform;
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            hudMissionManual = GameObject.Find("MissionManual").transform;
            playerCamera = GameObject.FindGameObjectWithTag("PlayerCam");
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
                    if (Input.GetKey(KeyCode.E))
                    {
                        transform.SetPositionAndRotation(spawnpointSdc.position, spawnpointSdc.rotation);
                    }
                    else
                    {
                        hud.SetInformationText("Appuie sur E pour passer dans la bouche d'aération");
                        hud.ShowText();
                    }
                }
                
                else if (!PhotonNetwork.IsMasterClient && hit.transform == electricalPanel)
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        ElectricalTask task = GameObject.Find("ElectricalTask").GetComponent<ElectricalTask>();
                        task.SetPlayerCamera(playerCamera);
                        task.SetPlayerMovement(playerMovement);
                        task.SetHUD(hudCanvas);
                        task.taskHUD.SetActive(true);
                        task.taskCamera.gameObject.SetActive(true);
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
                else if (PhotonNetwork.IsMasterClient &&
                         hit.transform == gameManager.boutonQuiOuvreLaPorteDeLaCellule.transform)
                {
                    if (Input.GetKey(KeyCode.U))
                    {
                        gameManager.OuvrirGrille();
                    }
                    else
                    {
                        hud.SetInformationText("Appuie sur U pour ouvrir la porte de la cellule de l'infiltré");
                        hud.ShowText();
                    }
                }
                else if (!PhotonNetwork.IsMasterClient && hit.transform.CompareTag("valves"))
                {
                    if (Input.GetKey(KeyCode.U))
                    {
                        GameObject.Find("salle eau").GetComponent<ValveTask>().TurnValve(hit.transform);
                    }
                    else
                    {
                        hud.SetInformationText("Appuie sur U pour actionner la valve");
                        hud.ShowText();
                    }
                }
                
                if (PhotonNetwork.IsMasterClient && hit.transform == hudMissionManual)
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        var task = GameObject.Find("MissionManual").GetComponent<MissionManual>();
                        task.playerCamera = playerCamera;
                        task.playerMovement = playerMovement;
                        task.taskCamera.gameObject.SetActive(true);
                        task.hud = hudCanvas;
                        hudCanvas.gameObject.SetActive(false);
                        playerCamera.SetActive(false);
                        playerMovement.enabled = false;
                    }
                    else
                    {
                        hud.SetInformationText("Appuie sur E pour consulter le manuel des missions");
                        hud.ShowText();
                    }
                }
            }
        }
    }
}
