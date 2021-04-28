using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Transform[] spawnpoints;
    
    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate("Player", spawnpoints[0].position, spawnpoints[0].rotation);
        else
            PhotonNetwork.Instantiate("Player", spawnpoints[1].position, spawnpoints[1].rotation);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
