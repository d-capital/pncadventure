using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : DialogueTrigger
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindObjectOfType<LanguageManager>().getCorrectName("npc", null, null, base.dialogue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
