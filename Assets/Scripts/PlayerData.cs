using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool enterCutSceneShown = false; 
    public int currentLevelIndex = 0; // 0 - main menu, 1 - cut scene 1, 2 - lvl 1, 3- lvl 2
    public string carma = "good"; //either "good" or "bad"
}
