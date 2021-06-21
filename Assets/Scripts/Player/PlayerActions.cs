using System;
using Menu;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class PlayerActions : MonoBehaviourPun
    {
        public GameObject playerCamera;
        public PlayerMovement playerMovement;
        
        public float maxReach;
        private HUD hud;
        private Transform spawnpointSdc;

        private Transform aerationVent;

        private GameManager gameManager;

        private void Start()
        {
            if (!photonView.IsMine)
                enabled = false;
            hud = GameObject.Find("HUD").GetComponent<HUD>();
            spawnpointSdc = GameObject.Find("Spawnpoint salle de contrôle").transform;
            aerationVent = GameObject.Find("aerationVent").transform;
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
                else if (PhotonNetwork.IsMasterClient &&
                         hit.transform == gameManager.boutonQuiOuvreLaPorteDeLaCellule.transform)
                {
                    if (Input.GetKey(KeyCode.E))
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
                    if (Input.GetKey(KeyCode.E))
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
            }
        }
    }
}
