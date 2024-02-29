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
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Resume", ResumeButtonText, null, "");
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "MainMenu", MainMenuButtonText, null, "");
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Quit", QuitButtonText, null, "");
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
        GameObject[] voicovers = GameObject.FindGameObjectsWithTag("voicover");
        if (voicovers.Length > 0)
        {
            foreach (GameObject voice in voicovers)
            {
                voice.GetComponent<AudioSource>().Stop();
            }
        }
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
