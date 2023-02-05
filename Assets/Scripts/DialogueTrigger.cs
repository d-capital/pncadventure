using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
	//public Animator plumberAnimator;

    void Start()
    {
		//dialogue.name = npcName; for the future, may be it will be better from tech design perspective
    }

	public void TriggerDialogue (string dialogueActor)
	{
        if (CanTriggerDialogue())
        {
			GameObject.FindObjectOfType<TasksButton>().BlockInventory();
			FindObjectOfType<DialogueManager>().StartDialogue(dialogue, dialogueActor);
		}
	}
	public void TriggerDialogueOldWomansBed(string dialogueActor)
	{
		if (CanTriggerDialogue())
		{
			GameObject.FindObjectOfType<TasksButton>().BlockInventory();
			FindObjectOfType<DialogueManager>().StartDialogueWithOldWomansBed(dialogue, dialogueActor);
		}
	}
	public void TriggerDialoguePlumber(string dialogueActor)
	{
		if (CanTriggerDialogue())
		{
			GameObject.FindObjectOfType<TasksButton>().BlockInventory();
			FindObjectOfType<DialogueManager>().StartDialogueWithPlumber(dialogue, dialogueActor);
		}
	}
	public void Answer(string dialogueActor, int incomingResponseIndex)
	{
		FindObjectOfType<DialogueManager>().Answer(dialogue, dialogueActor, incomingResponseIndex);
	}
	
	public bool CanTriggerDialogue()
    {
		bool CanTriggerDialogue = !GameObject.FindObjectOfType<PauseMenu>().isGamePaused
			&& GameObject.FindObjectOfType<TasksButton>().areTasksOpen == false;
		return CanTriggerDialogue;
    }


}