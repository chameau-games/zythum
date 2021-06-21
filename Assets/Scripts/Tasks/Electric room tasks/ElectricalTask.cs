using System.Collections;
using System.Collections.Generic;
using System;
using System.ComponentModel.Design;
using System.Linq;
using Menu;
using Player;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ElectricalTask : MonoBehaviour
{
    
    private int[][] possibilities =
    {
        new[] {-45, 0, 45, 45, -45, 45, -45, -45},
        new[] {-45, 45, 0, 0, -45, -45, 0, 45},
        new[] {45, 45, 45, 45, -45, -45, -45, -45},
    };

    private int[][] result =
    {
        new[] {1, 0, 0, 0},
        new[] {1, 0, 0, 1},
        new[] {0, 1, 1, 1}
    };

    private int[] actual = {1, 1, 1, 1};
    private int successNumber;
    
    
    private int index;
    public GameObject pointer1;
    public GameObject pointer2;
    public GameObject pointer3;
    public GameObject pointer4;
    public GameObject pointer5;
    public GameObject pointer6;
    public GameObject pointer7;
    public GameObject pointer8;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    public Animator animator;

    private GameObject playerCamera;
    private PlayerMovement playerMovement;
    public Camera taskCamera;
    public GameObject taskHUD;
    
    
    
    void Start()
    {
        System.Random rnd = new System.Random();
        index = rnd.Next(possibilities.Length);
        pointer1.transform.Rotate(0, possibilities[index][0], 0);
        pointer2.transform.Rotate(0, possibilities[index][1], 0);
        pointer3.transform.Rotate(0, possibilities[index][2], 0);
        pointer4.transform.Rotate(0, possibilities[index][3], 0);
        pointer5.transform.Rotate(0, possibilities[index][4], 0);
        pointer6.transform.Rotate(0, possibilities[index][5], 0);
        pointer7.transform.Rotate(0, possibilities[index][6], 0);
        pointer8.transform.Rotate(0, possibilities[index][7], 0);
        Debug.Log("gros zizi");
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            taskHUD.gameObject.SetActive(false);
            taskCamera.enabled = false;
            GameObject.Find("HUD").SetActive(true);
            Debug.Log(playerCamera.name);
            playerCamera.SetActive(true);
            playerMovement.enabled = true;
        }
    }
    
    public void OnClickButton(int number,string set, string unset)
    {
        if (actual[number] == 1)
        {
            animator.Play(unset);
            actual[number] = 0;
        }
        else
        {
            animator.Play(set);
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
            GameObject.Find("MissionList").GetComponent<MissionList>().TaskMachine();
        } 
    }
    
    public void OnClickButton1()
    {
        OnClickButton(0, "Set1", "Unset1");
    }

    public void OnClickButton2()
    {
        OnClickButton(1, "Set2","Unset2");
    }
    
    public void OnClickButton3()
    {
        OnClickButton(2, "Set3","Unset3");
    }
    
    public void OnClickButton4()
    {
        OnClickButton(3, "Set4","Unset4");
    }

    public void SetPlayerCamera(GameObject camera)
    {
        playerCamera = camera;
    }

    public void SetPlayerMovement(PlayerMovement movement)
    {
        playerMovement = movement;
    }
}
