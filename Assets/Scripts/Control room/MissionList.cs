using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionList : MonoBehaviour
{
    public TextMeshProUGUI textEau;
    public TextMeshProUGUI textInfo;
    public TextMeshProUGUI textMachine;
    
    public void TaskWater()
    {
        textEau.color = Color.green;
    }

    public void TaskServer()
    {
        textInfo.color = Color.green;
    }

    public void TaskMachine()
    {
        textMachine.color = Color.green;
    }
}
