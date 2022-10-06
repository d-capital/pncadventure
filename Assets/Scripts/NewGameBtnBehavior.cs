using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameBtnBehavior : MonoBehaviour
{
    public GameObject newGameBtn;
    // Start is called before the first frame update
    public void startNewGame()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        if(playerData != null)
        {
            GameObject canvasObj = GameObject.FindGameObjectWithTag("Canvas");
            Transform[] trs = canvasObj.GetComponentsInChildren<Transform>(true);
            foreach(Transform t in trs)
            {
                if(t.tag == "ConfirmationOvelray")
                {
                    t.gameObject.SetActive(true);
                    break;
                }
            }
        }
        else
        {
            PlayerData playerData1 = new PlayerData();
            playerData1.carma = "good";
            playerData1.currentLevelIndex = 1;
            playerData1.enterCutSceneShown = false;
            SaveSystem.SavePlayer(playerData1);
            SceneManager.LoadScene("Cut Scene 1");

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
