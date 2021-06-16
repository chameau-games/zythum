using TMPro;
using UnityEngine;

namespace Menu
{
    public class HUD : MonoBehaviour
    {

        public GameObject informationText;

        public void SetInformationText(string text)
        {
            informationText.GetComponent<TMP_Text>().text = text;
        }

        public void ShowText()
        {
            informationText.SetActive(true);
        }

        public void HideText()
        {
            informationText.SetActive(false);
        }
    }
}
