using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTEController : MonoBehaviour
{
    public float timeStart = 3;
    public TMP_Text timer;

    private void Start()
    {
        timer.text = timeStart.ToString();
    }

    private void Update()
    {
        if (timeStart > 0 && !Input.GetKeyDown(KeyCode.H))
        {
            Time.timeScale = 0.7f;
            timeStart -= Time.deltaTime;
            timer.text = Mathf.Round(timeStart).ToString();
        }
        else if (timeStart > 0 && Input.GetKeyDown(KeyCode.H))
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            PlayerData playerData = SaveSystem.LoadPlayer();
            playerData.currentLevelIndex = 12;
            GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>().LoadDistinctLevel(playerData,0);
        }
        else
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>().ReloadLevel();
        }

    }

}
