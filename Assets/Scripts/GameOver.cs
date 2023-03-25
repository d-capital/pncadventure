using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    string objectName = "Wasted";

    public GameObject GameOverScreen;

    public TMP_Text WastedText;
    public Text RestartButtonText;
    public Text MainMenuButtonText;
    public Text QuitButtonText;

    public bool isGameOverScreenShown = false;

    // Start is called before the first frame update
    void Start()
    {
        WastedText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "WastedText");
        RestartButtonText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Restart");
        MainMenuButtonText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "MainMenu");
        QuitButtonText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Quit");
    }

    public void RestartLevel()
    {
        isGameOverScreenShown = false;
        GameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().name != "Bossfight")
        {
            GameObject.FindObjectOfType<DialogueManager>().UnblockTasksAndInventory();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        isGameOverScreenShown = false;
        GameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().name != "Bossfight")
        {
            GameObject.FindObjectOfType<DialogueManager>().UnblockTasksAndInventory();
        }
        Debug.Log("Quiting Game...");
        Application.Quit();

    }

    public void ToMainMenu()
    {
        isGameOverScreenShown = false;
        GameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().name != "Bossfight")
        {
            GameObject.FindObjectOfType<DialogueManager>().UnblockTasksAndInventory();
        }
        SceneManager.LoadScene(0);

    }

    public void ShowGameOverScreen()
    {
        GameOverScreen.SetActive(true);
        isGameOverScreenShown = true;
        Time.timeScale = 0f;
        if (SceneManager.GetActiveScene().name != "Bossfight")
        {
            GameObject.FindObjectOfType<DialogueManager>().BlockTasksAndInventory();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
