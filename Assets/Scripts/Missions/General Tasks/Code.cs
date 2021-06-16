using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Code : MonoBehaviour
{
    public bool isLocked = false;
    public string input = "";
    public int iRoom = new Random().Next(20);
    public string[] codes = {"","","",""};
    
    //faire le digicode avec tt les game object
    public void OnClickOneButton()
    {
        input += "1";
    }
    public void OnClickTwoButton()
    {
        input += "2";
    }
    public void OnClickThreeButton()
    {
        input += "3";
    }
    public void OnClickFourButton()
    {
        input += "4";
    }
    public void OnClickFiveButton()
    {
        input += "5";
    }
    public void OnClickSixButton()
    {
        input += "6";
    }
    public void OnClickSevenButton()
    {
        input += "7";
    }
    public void OnClickHeightButton()
    {
        input += "8";
    }
    public void OnClickNineButton()
    {
        input += "9";
    }
    public void OnClickDelButton()
    {
        if (input.Length != 0)
            input = input.Remove(input.Length - 1);
    }
    public void OnClickValidButton()
    {
        
    }
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        input = "";
    }

    // Update is called once per frame
    void Update()
    {
        isLocked = input == codes[iRoom];
    }
}
