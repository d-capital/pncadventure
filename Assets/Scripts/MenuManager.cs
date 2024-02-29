using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Text;

public class MenuManager : MonoBehaviour
{
    string objectName = "MainMenu";

    public Text resumeText;
    public Text quitText;
    public Text newGameText;

    public TMP_Dropdown languageDropdown;

    public bool wasLanguageSet = false;

    public string jsonString;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        wasLanguageSet = false;
    }
    
    void OnLevelFinishedLoading()
    {
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "resume", resumeText, null, "");
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "quit", quitText, null, "");
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "newGame", newGameText, null, "");
        languageDropdown.value = GameObject.FindObjectOfType<LanguageManager>().getCorrectDropDownValue();
        wasLanguageSet = true;
    }

    public void Update()
    {
        if (Time.timeSinceLevelLoad > 0.1 && !wasLanguageSet)
        {
            OnLevelFinishedLoading();
        }
    }

    public void startNewGame()
    {
        GameObject.FindObjectOfType<NewGameBtnBehavior>().startNewGame();

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
        string prevLan = "";
        if (pd1 != null)
        {
            prevLan = pd1.language;
        }
        else
        {
            prevLan = "ru";
        }
        PlayerData pd = SaveSystem.LoadPlayer();
        if (pd == null)
        {
            pd = new PlayerData();
        }
        if (languageDropdown.value == 0)
        {
            
            pd.language = "ru";
            SaveSystem.SavePlayer(pd);
            if(prevLan != "ru")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
         
        }
        else if(languageDropdown.value == 1)
        {
            pd.language = "en";
            SaveSystem.SavePlayer(pd);
            if (prevLan != "en")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
