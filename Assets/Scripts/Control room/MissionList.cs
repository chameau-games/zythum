using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionList : MonoBehaviour
{
    public TextMeshProUGUI textEau;
    public TextMeshProUGUI textInfo;
    public TextMeshProUGUI textMachine;
    
    public GameObject playerCamera;
    public GameObject taskCamera;
    public GameObject taskHUD;

    private int missionNumber = 3;
    public void TaskWater()
    {
        textEau.color = Color.green;
        missionNumber--;
    }

    public void TaskServer()
    {
        textInfo.color = Color.green;
        missionNumber--;
    }
    
    public void TaskMachine()
    {
        textMachine.color = Color.green;
        missionNumber--;
    }
}
