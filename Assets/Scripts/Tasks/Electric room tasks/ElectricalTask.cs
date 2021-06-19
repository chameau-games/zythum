using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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

    private int index;
    public GameObject pointer1;
    public GameObject pointer2;
    public GameObject pointer3;
    public GameObject pointer4;
    public GameObject pointer5;
    public GameObject pointer6;
    public GameObject pointer7;
    public GameObject pointer8;

    public Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        System.Random rnd = new System.Random();
        index = rnd.Next(possibilities.Length);
        pointer1.transform.Rotate(possibilities[index][1], 0, 0);
        pointer2.transform.Rotate(possibilities[index][2], 0, 0);
        pointer3.transform.Rotate(possibilities[index][3], 0, 0);
        pointer4.transform.Rotate(possibilities[index][4], 0, 0);
        pointer5.transform.Rotate(possibilities[index][5], 0, 0);
        pointer6.transform.Rotate(possibilities[index][6], 0, 0);
        pointer7.transform.Rotate(possibilities[index][7], 0, 0);
        pointer8.transform.Rotate(possibilities[index][8], 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
