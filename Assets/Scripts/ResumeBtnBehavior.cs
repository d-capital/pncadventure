using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeBtnBehavior : MonoBehaviour
{
    public GameObject resumeBtn;
    // Start is called before the first frame update
    void Start()
    {
        
        PlayerData playerData = SaveSystem.LoadPlayer();
        if (playerData == null || playerData.currentLevelIndex == 0)
        {
            resumeBtn.SetActive(false);
        }
        else
        {
            resumeBtn.SetActive(true);
        }

    }
}
