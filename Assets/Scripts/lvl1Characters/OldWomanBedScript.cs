using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldWomanBedScript : DialogueTrigger
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindObjectOfType<LanguageManager>().getCorrectName("OldWomansBed", null, null, base.dialogue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
