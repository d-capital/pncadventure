using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public bool isGamePaused = false;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        AdvScript[] advScripts = Resources.FindObjectsOfTypeAll<AdvScript>();
        string currentLevel = advScripts[0].GetComponent<AdvScript>().GetCurrentLevel();

        return currentLevel;
    }

}
