using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    string objectName = "MainMenu";

    public Text resumeText;
    public Text quitText;
    public Text newGameText;

    public TMP_Dropdown languageDropdown;

    public bool wasLanguageSet = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        wasLanguageSet = false;
    }
    
    void OnLevelFinishedLoading()
    {
        resumeText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "resume");
        quitText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "quit");
        newGameText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "newGame");
        languageDropdown.value = GameObject.FindObjectOfType<LanguageManager>().getCorrectDropDownValue();
        wasLanguageSet = true;
    }

    public void Update()
    {
        if (Time.timeSinceLevelLoad > 0.5 && !wasLanguageSet)
        {
            OnLevelFinishedLoading();
        }
    }

    public void startNewGame()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        if(playerData != null)
        {
            OverlayManager Overlay = GameObject.FindObjectOfType<OverlayManager>();
            Overlay.Overlay.gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Cut Scene 1");
            playerData.currentLevelIndex = 1;
            playerData.enterCutSceneShown = false;
            SaveSystem.SavePlayer(playerData);
        }

    }

    public void ResumeGame()
    {
       PlayerData playerData = SaveSystem.LoadPlayer();
       if(playerData != null)
       {
        SceneManager.LoadScene(playerData.currentLevelIndex);
       }
    }

    public void ExitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }

    public void SetNewLanguage()
    {
        PlayerData pd1 = SaveSystem.LoadPlayer();
        string prevLan = pd1.language;
        if(languageDropdown.value == 0)
        {
            PlayerData pd = SaveSystem.LoadPlayer();
            pd.language = "ru";
            SaveSystem.SavePlayer(pd);
            if(prevLan != "ru")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
         
        }
        else if(languageDropdown.value == 1)
        {
            PlayerData pd = SaveSystem.LoadPlayer();
            pd.language = "en";
            SaveSystem.SavePlayer(pd);
            if (prevLan != "en")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
