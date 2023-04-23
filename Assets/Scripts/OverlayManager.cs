using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverlayManager : MonoBehaviour
{
    public GameObject Overlay;
    public Text yesText;
    public Text noText;
    public Text overlayText;

    string objectName = "MainMenuOverlay";

    // Start is called before the first frame update
    void Start()
    {
        //Overlay.SetActive(false);
        yesText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "yes");
        noText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "no");
        overlayText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "text");
    }

    public void ShowOverlay()
    {
        Overlay.SetActive(true);
    }
    public void DeclineOverlay()
    {
       Overlay.SetActive(false);
    }

    public void AcceptOverlay()
    {
        PlayerData playerData = new PlayerData();
        SceneManager.LoadScene("Cut Scene 0");
        playerData.currentLevelIndex = 1;
        playerData.enterCutSceneShown = false;
        SaveSystem.SavePlayer(playerData);
    }
}
