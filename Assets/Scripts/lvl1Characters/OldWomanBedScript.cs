using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldWomanBedScript : DialogueTrigger
{
    // Start is called before the first frame update
    void Start()
    {
        base.dialogue.name = GameObject.FindObjectOfType<LanguageManager>().getCorrectName("OldWomansBed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
