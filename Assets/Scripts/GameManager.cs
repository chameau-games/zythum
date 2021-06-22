using Photon.Pun;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{

    public Transform[] spawnpoints;
    public GameObject grille;
    public GameObject boutonQuiOuvreLaPorteDeLaCellule;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Player", spawnpoints[0].position, spawnpoints[0].rotation);
            GameObject[] tousLesBoutonsSDC = GameObject.FindGameObjectsWithTag("bouton sdc");
            boutonQuiOuvreLaPorteDeLaCellule = tousLesBoutonsSDC[new Random().Next(tousLesBoutonsSDC.Length)];
            
        }
        else
            PhotonNetwork.Instantiate("Player", spawnpoints[1].position, spawnpoints[1].rotation);
        

    }

    public void OuvrirGrille()
    {
        grille.GetComponent<Animator>().SetBool("open", true);
    }

}
