using System.Collections;
using System.Collections.Generic;
using System;
using System.ComponentModel.Design;
using System.Linq;
using Menu;
using Photon.Pun;
using Player;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ElectricalTask : MonoBehaviourPun
{

    private int[][] possibilities =
    {
        new[] {-45, 45, 0, 45, -45, 45, 45, 45, 0, -45, -45, 0},
        new[] {-45, 45, 0, 0, 45, -45, 0, -45, -45, 45, -45, 0},
        new[] {45, 45, 45, -45, 45, 0, 45, 45, -45, 0, 45, 45},
        new[] {45, -45, 45, -45, -45, 0, 0, -45, 45, 45, 45, -45}
    };

    private int[][] result =
    {
        new[] {0, 1, 1, 1},
        new[] {0, 0, 1, 0},
        new[] {1, 0, 0, 1},
        new[] {1, 1, 0, 0}
    };

    private int[] actual = {1, 1, 1, 1};
    private int successNumber;
    
    
    private int index;
    public GameObject pointer11;
    public GameObject pointer12;
    public GameObject pointer13;
    public GameObject pointer21;
    public GameObject pointer22;
    public GameObject pointer23;
    public GameObject pointer31;
    public GameObject pointer32;
    public GameObject pointer33;
    public GameObject pointer41;
    public GameObject pointer42;
    public GameObject pointer43;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    public GameObject lever1;
    public GameObject lever2;
    public GameObject lever3;
    public GameObject lever4;
    

    private GameObject playerCamera;
    private PlayerMovement playerMovement;
    public Camera taskCamera;
    public GameObject taskHUD;
    private GameObject hud;
    
    
    
    void Start()
    {
        System.Random rnd = new System.Random();
        index = rnd.Next(possibilities.Length);
        pointer11.transform.Rotate(0, possibilities[index][0], 0);
        pointer12.transform.Rotate(0, possibilities[index][1], 0);
        pointer13.transform.Rotate(0, possibilities[index][2], 0);
        pointer21.transform.Rotate(0, possibilities[index][3], 0);
        pointer22.transform.Rotate(0, possibilities[index][4], 0);
        pointer23.transform.Rotate(0, possibilities[index][5], 0);
        pointer31.transform.Rotate(0, possibilities[index][6], 0);
        pointer32.transform.Rotate(0, possibilities[index][7], 0);
        pointer33.transform.Rotate(0, possibilities[index][8], 0);
        pointer41.transform.Rotate(0, possibilities[index][9], 0);
        pointer42.transform.Rotate(0, possibilities[index][10], 0);
        pointer43.transform.Rotate(0, possibilities[index][11], 0);
        for (int i = 0; i < result.Length; i++)
        {
            if (result[index][i] == actual[i])
            {
                successNumber++;
            }
        } 
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            taskHUD.SetActive(false);
            taskCamera.gameObject.SetActive(false);
            hud.SetActive(true);
            playerCamera.SetActive(true);
            playerMovement.enabled = true;
        }
    }
    
    public void OnClickButton(int number, GameObject lever)
    {
        Animator anim = lever.gameObject.GetComponent<Animator>();
        if (actual[number] == 1)
        {
            anim.SetBool("set", false);
            anim.Play("unset");
            actual[number] = 0;
        }
        else
        {
            anim.SetBool("set",true);
            actual[number] = 1;
        }

        if (actual[number] == result[index][number])
        {
            successNumber++;
        }
        else
        {
            successNumber--;
        }

        if (successNumber == result.Length)
        {
            photonView.RPC("success", RpcTarget.All);
        } 
    }

    [PunRPC]
    public void success()
    {
        GameObject.Find("MissionList").GetComponent<MissionList>().TaskMachine();
    }
    
    public void OnClickButton1()
    {
        OnClickButton(0, lever1);
    }

    public void OnClickButton2()
    {
        OnClickButton(1, lever2);
    }
    
    public void OnClickButton3()
    {
        OnClickButton(2, lever3);
    }
    
    public void OnClickButton4()
    {
        OnClickButton(3, lever4);
    }

    public void SetPlayerCamera(GameObject camera)
    {
        playerCamera = camera;
    }

    public void SetPlayerMovement(PlayerMovement movement)
    {
        playerMovement = movement;
    }

    public void SetHUD(GameObject HUD)
    {
        hud=HUD;
    }
}
