using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Code : MonoBehaviour
{
    public GameObject playerCam;
    public bool isLocked = true;
    public string input = "";
    public TMP_Text code = null;
    public int idRoom = new Random().Next(20);
    private string[] codes = {
            "3726","1845","2759","2802","5889","9493","1896","7380",
            "6187","9370","7480","4999","9908","4507","4447","1649",
            "2526","6660","5325","7350"
        };
    

    public void OnClickNumButton(string num)
    {
        if (input.Length < 4)
            input += num;
        code.text = input;
    }
    public void OnClickDelButton()
    {
        if (input.Length != 0)
            input = input.Remove(input.Length - 1);
        code.text = input;
    }
    public void OnClickValidButton()
    {
        if (!isLocked) return;
        if (input == codes[idRoom])
        {
            isLocked = false;
        }
        else
        {
            input = "";
            code.text = input;
        }
    }
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
