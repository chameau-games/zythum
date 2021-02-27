using System;
using Mirror;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    private ScenesManager _sceneManager;
    private NetworkManager _networkManager;

    public void Start()
    {
        _sceneManager = GameObject.Find("ScenesManager").GetComponent<ScenesManager>();
        _networkManager = NetworkManager.singleton;
        _networkManager.networkAddress = "localhost";
    }

    public void JoinGame()
    {
        _networkManager.StartClient();
    }

    public void HostGame()
    {
        _networkManager.StartHost(); 
    }

    public void Back()
    {
        _sceneManager.SwitchScene("MainMenu");
    }
}
