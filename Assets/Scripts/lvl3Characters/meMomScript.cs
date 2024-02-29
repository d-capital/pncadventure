using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meMomScript : DialogueTrigger
{
    public GameObject NpcObject;
    public Animator npcAnimator;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindObjectOfType<LanguageManager>().getCorrectName("MeMom", null, null, base.dialogue);
    }

    // Update is called once per frame
    void Update()
    {

    }
}