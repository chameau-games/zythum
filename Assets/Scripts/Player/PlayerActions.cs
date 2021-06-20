using System;
using Menu;
using Photon.Pun;
using Tasks;
using UnityEngine;

namespace Player
{
    public class PlayerActions : MonoBehaviour
    {
        private int code;
        public float maxReach;
        private HUD hud;
        private Transform spawnpointSdc;

        private Transform aerationVent;

        private GameManager gameManager;

        private void Start()
        {
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
            }
        }
    }
}
