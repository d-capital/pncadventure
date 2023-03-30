using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    string objectName = "PauseMenu";

    public Text ResumeButtonText;
    public Text MainMenuButtonText;
    public Text QuitButtonText;

    public bool isGamePaused = false;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        ResumeButtonText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Resume");
        MainMenuButtonText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "MainMenu");
        QuitButtonText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Quit");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameObject.FindObjectOfType<GameOver>().isGameOverScreenShown)
        {
            if(isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        string currentLevel = GetCurrentLevel();
        if (currentLevel != "Bossfight")
        {
            GameObject.FindObjectOfType<DialogueManager>().UnblockTasksAndInventory();
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        string currentLevel = GetCurrentLevel();
        if (currentLevel != "Bossfight")
        {
            GameObject.FindObjectOfType<DialogueManager>().BlockTasksAndInventory();
        }   
    }
    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        isGamePaused = false;
        Application.Quit();
    }
    public void MainMenu()
    {
       pauseMenuUI.SetActive(false);
       Time.timeScale = 1f;
       SceneManager.LoadScene(0);
       isGamePaused = false;
    }

    public string GetCurrentLevel()
    {
        
        string currentLevel = SceneManager.GetActiveScene().name;

        return currentLevel;
    }

}
