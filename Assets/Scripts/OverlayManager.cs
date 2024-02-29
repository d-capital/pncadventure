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
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "yes", yesText, null, "");
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "no", noText, null, "");
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "text", overlayText, null, "");
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
        PlayerData prevPlayerData = SaveSystem.LoadPlayer();
        PlayerData playerData = new PlayerData();
        playerData.language = prevPlayerData.language;
        playerData.currentLevelIndex = 1;
        playerData.enterCutSceneShown = false;
        SaveSystem.SavePlayer(playerData);
        SceneManager.LoadScene("Cut Scene 0");
    }
}
