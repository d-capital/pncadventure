using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameBtnBehavior : MonoBehaviour
{
    public GameObject newGameBtn;
    // Start is called before the first frame update
    public void startNewGame()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        if(playerData != null && playerData.currentLevelIndex > 0)
        {
            OverlayManager[] OverlayManagers =  Resources.FindObjectsOfTypeAll<OverlayManager>();
            foreach(OverlayManager om in OverlayManagers)
            {
                om.gameObject.SetActive(true);
            }
        }
        else
        {
            PlayerData playerData1 = new PlayerData();
            playerData1.currentLevelIndex = 1;
            playerData1.enterCutSceneShown = false;
            SaveSystem.SavePlayer(playerData1);
            SceneManager.LoadScene("Cut Scene 0");

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
