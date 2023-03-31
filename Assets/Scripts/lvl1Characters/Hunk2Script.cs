using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunk2Script : DialogueTrigger
{
    public Animator npcAnimator;
    // Start is called before the first frame update
    void Start()
    {
        base.dialogue.name = GameObject.FindObjectOfType<LanguageManager>().getCorrectName("Hunk2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
