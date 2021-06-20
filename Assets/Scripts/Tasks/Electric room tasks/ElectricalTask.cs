using System.Collections;
using System.Collections.Generic;
using System;
using System.ComponentModel.Design;
using System.Linq;
using Menu;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ElectricalTask : MonoBehaviour
{
    
    private int[][] possibilities =
    {
        new[] {-45, 0, 45, 45, -45, 45, -45, -45},
        new[] {0, 45, 0, 0, -45, -45, 0, 45},
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

    public GameObject playerCamera;
    public GameObject taskCamera;
    public GameObject taskHUD;

    private bool success = false;
    
    void Start()
    {
        System.Random rnd = new System.Random();
        index = rnd.Next(possibilities.Length);
        pointer1.transform.Rotate(possibilities[index][0], 0, 0);
        pointer2.transform.Rotate(possibilities[index][1], 0, 0);
        pointer3.transform.Rotate(possibilities[index][2], 0, 0);
        pointer4.transform.Rotate(possibilities[index][3], 0, 0);
        pointer5.transform.Rotate(possibilities[index][4], 0, 0);
        pointer6.transform.Rotate(possibilities[index][5], 0, 0);
        pointer7.transform.Rotate(possibilities[index][6], 0, 0);
        pointer8.transform.Rotate(possibilities[index][7], 0, 0);
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
        if (Input.GetKey(KeyCode.Escape))
        {
            taskHUD.gameObject.SetActive(false);
            taskCamera.gameObject.SetActive(false);
            playerCamera.gameObject.SetActive(true);
            GameObject.Find("HUD").SetActive(true);
        }
    }
    
    public void OnClickButton(int number)
    {
        if (actual[number] == 1)
        {
            animator.Play("Unset"+(number+1));
            actual[number] = 0;
        }
        else
        {
            animator.Play("Set"+(number+1));
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
            success = true;
        } 
    }
    
    public void OnClickButton1()
    {
        OnClickButton(0);
    }

    public void OnClickButton2()
    {
        OnClickButton(1);
    }
    
    public void OnClickButton3()
    {
        OnClickButton(2);
    }
    
    public void OnClickButton4()
    {
        OnClickButton(3);
    }

    public bool GetSuccess()
    {
        return success;
    }
}
