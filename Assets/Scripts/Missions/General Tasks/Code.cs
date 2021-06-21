using Photon.Pun;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Code : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject digicodeCam;
    public PlayerMovement playerMov;
    public GameObject hud;
    public bool onCamDigi;
    public bool isLocked = true;
    private string input = "";
    public TMP_Text code;
    private int idRoom;
    public TMP_Text id;
    private string[] codes = {
            "3726","1845","2759","2802","5889","9493","1896","7380",
            "6187","9370","7480","4999","9908","4507","4447","1649",
            "2526","6660","5325","7350"
        };



    public void OnClickNumButton(string num)
    {
        if (input.Length < 4)
            input += num;
        code.text = input;
    }
    public void OnClickDelButton()
    {
        if (input.Length != 0)
            input = input.Remove(input.Length - 1);
        code.text = input;
    }
    public void OnClickValidButton()
    {
        if (!isLocked) return;
        if (input == codes[idRoom])
        {
            isLocked = false;
        }
        else
        {
            input = "";
            code.text = input;
        }
    }
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        idRoom = new Random().Next(20);
        string numDigicode = idRoom.ToString();
        if (numDigicode.Length < 2)
            numDigicode = '0' + numDigicode;
        id.text = numDigicode;
        Debug.LogError("bbb");
        Debug.LogError(code.text);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (onCamDigi && Input.GetKey(KeyCode.E))
        {
            playerCam.SetActive(true);
            playerMov.enabled = true;
        }*/
    }
}
