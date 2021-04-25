using Mirror;
using Multi;
using Player;
using UnityEngine;
using Vivox;

public class GameManager : NetworkBehaviour
{
    private static GameManager _instance;
    public static GameManager singleton { get { return _instance; } }

    [HideInInspector] [SyncVar] public string vivoxChannelName;

    [HideInInspector] public GameObject localPlayer;

    private bool _isPlayerActivated = false;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    [TargetRpc]
    public void TargetAssignLocalPlayer(NetworkConnection networkConnection, GameObject lp)
    {
        localPlayer = lp;
    }

    [TargetRpc]
    public void TargetConnectToVivox(NetworkConnection networkConnection)
    {
        VivoxManager vivox = GameObject.Find("VivoxManager").GetComponent<VivoxManager>();
        vivox.Login();
    }

    [Command(ignoreAuthority = true)]
    public void CmdAskForStart()
    {
        if (CustomNetworkManager.singleton.mode == NetworkManagerMode.Host)
        {
            CustomNetworkManager.singleton.ServerChangeScene("Underground");
            RpcActivatePlayer();
        }
    }

    [ClientRpc]
    public void RpcActivatePlayer()
    {
        Debug.Log("activate");
        localPlayer.transform.Find("Camera").gameObject.SetActive(true);
        localPlayer.GetComponent<PlayerMovement>().enabled = true;
        _isPlayerActivated = true;
    }

    public void DeactivatePlayer()
    {
        if(!_isPlayerActivated)
            return;
        localPlayer.transform.Find("Camera").gameObject.SetActive(false);
        localPlayer.GetComponent<PlayerMovement>().enabled = false;
    }
}