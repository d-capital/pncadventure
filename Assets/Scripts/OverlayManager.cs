using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverlayManager : MonoBehaviour
{
    public GameObject Overlay;
    // Start is called before the first frame update
    void Start()
    {
        Overlay.SetActive(false);
    }

    public void ShowOverlay()
    {
        Overlay.SetActive(true);
    }
    public void DeclineOverlay()
    {
       Overlay.SetActive(false);
    }

    public void AcceptOverlay()
    {
        PlayerData playerData = new PlayerData();
        SceneManager.LoadScene("Cut Scene 1");
        playerData.currentLevelIndex = 1;
        playerData.enterCutSceneShown = false;
        SaveSystem.SavePlayer(playerData);
    }
}
