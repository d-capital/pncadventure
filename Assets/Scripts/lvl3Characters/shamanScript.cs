using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shamanScript : DialogueTrigger
{
    public GameObject NpcObject;
    public Animator npcAnimator;
    // Start is called before the first frame update
    void Start()
    {
        base.dialogue.name = GameObject.FindObjectOfType<LanguageManager>().getCorrectName("Shaman");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
