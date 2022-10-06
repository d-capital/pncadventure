using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoText : MonoBehaviour
{
    public GameObject infoText;
    public TextMeshProUGUI infoTextString;
    // Start is called before the first frame update
    void Start()
    {
        infoText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowInfoText(string textToShow)
    {
        infoText.SetActive(true);
        if (infoTextString)
        {
            infoTextString.text = textToShow;
        }
        
        StartCoroutine(hideInfoTextWithDelay());
    }

    public void CloseInfoText()
    {
        infoText.SetActive(false);
    }
    IEnumerator hideInfoTextWithDelay()
    {
        yield return new WaitForSeconds(5.0f);
        infoText.SetActive(false);
    }
}
