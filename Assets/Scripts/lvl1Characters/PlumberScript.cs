using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlumberScript : DialogueTrigger
{
    public Animator npcAnimator;
    //public string npcName;

    // Start is called before the first frame update
    void Start()
    {
        npcAnimator.SetBool("isHappy", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changePlumberAnimationToHappy()
	{
		npcAnimator.SetBool("isHappy", true);
	}
}
