using Menu;
using Photon.Pun;
using Tasks;
using UnityEngine;

namespace Player
{
    public class PlayerActions : MonoBehaviourPun
    {
        public GameObject playerCamera;
        public PlayerMovement playerMovement;
        
        public float maxReach;
        private HUD hud;
        private GameObject hudCanvas;
        private Transform spawnpointSdc;

        private Transform aerationVent;
        private Transform electricalPanel;
        private Transform hudMissionManual;

        private Transform card;

        private Transform carte;
        private GameManager gameManager;

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
            aerationVent = GameObject.Find("aerationVent").transform;
            hudMissionManual = GameObject.Find("MissionManual").transform;
            playerCamera = GameObject.FindGameObjectWithTag("PlayerCam");
            card = GameObject.Find("carte").transform;
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
                    if (Input.GetKeyDown(KeyCode.E))
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
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        gameManager.OuvrirGrille();
                    }
                    else
                    {
                        hud.SetInformationText("Appuie sur E pour ouvrir la porte de la cellule de l'infiltré");
                        hud.ShowText();
                    }
                }
                else if (hit.transform.gameObject.CompareTag("digicode"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Code digicode = hit.transform.parent.GetComponent<Code>();
                        digicode.playerCam = playerCamera;
                        digicode.playerMov = playerMovement;
                        digicode.hud = hud.gameObject;
                        digicode.digicodeCam.SetActive(true);
                        digicode.digicodeCanvas.SetActive(true);
                        
                        hud.gameObject.SetActive(false);
                        playerCamera.SetActive(false);
                        playerMovement.enabled = false;
                    }
                    else
                    {
                        hud.SetInformationText("Appuie sur E pour regarder de plus près le digicode");
                        hud.ShowText();
                    }
                }
                else if (hit.transform.gameObject.CompareTag("serverTag"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        ServerTask serverTask = hit.transform.parent.GetComponent<ServerTask>();
                        serverTask.playerCam = playerCamera;
                        serverTask.playerMov = playerMovement;
                        playerCamera.SetActive(false);
                        playerMovement.enabled = false;
                        hit.transform.parent.Find("Camera Serveur").gameObject.SetActive(true);
                    }
                    else
                    {
                        hud.SetInformationText("Appuie sur E pour réparer le rack");
                        hud.ShowText();
                    }
                }
                else if (!PhotonNetwork.IsMasterClient && hit.transform.CompareTag("valves"))
                {
                    if (Input.GetKeyDown(KeyCode.U))
                    {
                        GameObject.Find("salle eau").GetComponent<ValveTask>().TurnValve(hit.transform);
                    }
                    else
                    {
                        hud.SetInformationText("Appuie sur U pour actionner la valve");
                        hud.ShowText();
                    }
                }
                
                else if (PhotonNetwork.IsMasterClient && hit.transform == hudMissionManual)
                {
                    if (Input.GetKeyDown(KeyCode.E))
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
                
                else if (hit.transform == card)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        card.gameObject.SetActive(false);
                    }
                    else
                    {
                        hud.SetInformationText("Appuie sur E pour récupérer la carte");
                        hud.ShowText();
                    }
                }
            }
        }
    }
}
