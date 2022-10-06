using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
	public Animator? plumberAnimator;

    void Start()
    {
        plumberAnimator.SetBool("isHappy", false);
    }

	public void TriggerDialogue (string dialogueActor)
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue, dialogueActor);
	}
	public void TriggerDialogueOldWomansBed(string dialogueActor)
	{
		FindObjectOfType<DialogueManager>().StartDialogueWithOldWomansBed(dialogue, dialogueActor);
	}
	public void TriggerDialoguePlumber(string dialogueActor)
	{
		FindObjectOfType<DialogueManager>().StartDialogueWithPlumber(dialogue, dialogueActor);
	}
	public void Answer(string dialogueActor, int incomingResponseIndex)
	{
		FindObjectOfType<DialogueManager>().Answer(dialogue, dialogueActor, incomingResponseIndex);
	}
	public void changePlumberAnimationToHappy()
	{
		plumberAnimator.SetBool("isHappy", true);
	}

}