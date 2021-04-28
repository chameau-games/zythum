using System.Collections;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Transform[] spawnpoints;
    public GameObject grille;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Player", spawnpoints[0].position, spawnpoints[0].rotation);
            StartCoroutine(OpenDoorIn5Seconds());
        }
        else
            PhotonNetwork.Instantiate("Player", spawnpoints[1].position, spawnpoints[1].rotation);
        

    }

    IEnumerator OpenDoorIn5Seconds()
    {
        yield return new WaitForSeconds(5);
        
        grille.GetComponent<Animator>().SetBool("isOpening", true);
        
    }

}
