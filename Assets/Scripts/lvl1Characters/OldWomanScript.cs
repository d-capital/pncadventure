using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldWomanScript : DialogueTrigger
{
    public Animator npcAnimator;
    // Start is called before the first frame update
    void Start()
    {
        npcAnimator.SetBool("isSleeping", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeOldWomanAnimationToSleeping()
	{
		npcAnimator.SetBool("isSleeping", true);
	}
}
