using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    
    private AI_patroling ptrl;
    public Camera winCamera;
    public Camera gameoverCamera;
    private GameObject[] players;
    // Start is called before the first frame update
    void Start()
    {
        ptrl =GameObject.FindObjectOfType<AI_patroling>();
        players=GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            p.transform.Find("Camera").gameObject.SetActive(false);
        }
        if (ptrl.gameover)
        {
            gameoverCamera.gameObject.SetActive(true);
            winCamera.gameObject.SetActive(false);
        }
        else
        {
            gameoverCamera.gameObject.SetActive(false);
            winCamera.gameObject.SetActive(true);
        }
    }
}
