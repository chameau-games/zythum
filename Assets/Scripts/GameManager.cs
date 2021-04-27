using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(0f, 1f, 0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
