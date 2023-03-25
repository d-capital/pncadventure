using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    string objectName = "MainMenu";

    public Text resumeText;
    public Text quitText;
    public Text newGameText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        resumeText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "resume");
        quitText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "quit");
        newGameText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "newGame");
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
}
