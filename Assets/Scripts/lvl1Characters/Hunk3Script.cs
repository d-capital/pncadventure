using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunk3Script : DialogueTrigger
{
    public Animator npcAnimator;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindObjectOfType<LanguageManager>().getCorrectName("Hunk3", null, null, base.dialogue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
