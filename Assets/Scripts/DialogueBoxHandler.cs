using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBoxHandler : MonoBehaviour
{
    public GameObject dialogueBox;

    //public GameObject[] AnswerButtons;
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    public void ShowDialogue()
    {
        dialogueBox.SetActive(true);
    }

    public void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
    }

    public void SkipVoicover()
    {
        GameObject[] voicovers = GameObject.FindGameObjectsWithTag("voicover");
        if(voicovers.Length > 0)
        {
            foreach(GameObject voice in voicovers)
            {
                voice.GetComponent<AudioSource>().Stop();
            }
        }
    }
}
