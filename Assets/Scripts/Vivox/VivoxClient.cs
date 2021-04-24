using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;

public class VivoxClient : MonoBehaviour
{
    private VivoxUnity.Client client;
    private string _id = SystemInfo.deviceUniqueIdentifier;
    private void Awake()
    {
        client = new Client();
        client.Uninitialize();
        client.Initialize();
        DontDestroyOnLoad(this);
    }
    
    private void OnApplicationQuit()
    {
        client.Uninitialize();
    }
}
