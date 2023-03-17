using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void startNewGame()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        if(playerData != null)
        {
            OverlayManager Overlay = GameObject.FindObjectOfType<OverlayManager>();
            Overlay.Overlay.gameObject.SetActive(true);
            //Transform[] trs = canvasObj.GetComponentsInChildren<Transform>(true);
            //foreach(Transform t in trs)
           // {
             //   if(t.tag == "ConfirmationOvelray")
              //  {
                //    t.gameObject.SetActive(true);
              //  }
           // }
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
