using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class milfScript : DialogueTrigger
{
    public GameObject NpcObject;
    public Animator npcAnimator;


    public bool isMoving = false;
    public bool isScriptActive = false;
    public bool isMilfStanding = false;
    public bool targetCorridorNearSit = false;
    public bool targetNextCar = false;
    public string currentMission;
    public float speed;

    Vector2 targetPos;
    Vector2 corridorNearMilfSit;
    Vector2 nextCar;

    public bool isOnTarget;

    public bool xScaleWasChanged = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindObjectOfType<LanguageManager>().getCorrectName("Milf", null, null, base.dialogue);
        npcAnimator.SetBool("hasToStandUp", false);
        npcAnimator.SetBool("hasToWalk", false);
    }

    // Update is called once per frame
    void Update()
    {
        corridorNearMilfSit = GameObject.FindGameObjectWithTag("corridorNearMilfSit").transform.position;
        nextCar = GameObject.FindGameObjectWithTag("nextCar").transform.position;
        if (isScriptActive)
        {
            if (isMilfStanding)
            {
                if (targetCorridorNearSit && !targetNextCar
                    && !(corridorNearMilfSit.x == NpcObject.transform.position.x 
                    && corridorNearMilfSit.y == NpcObject.transform.position.y))
                {
                    targetPos = corridorNearMilfSit;
                    isMoving = true;
                    Debug.Log("currentMission is " + currentMission + "character pos" + NpcObject.transform.position + " target pos" + targetPos);
                }
                else if(targetCorridorNearSit && !targetNextCar
                    && (corridorNearMilfSit.x == NpcObject.transform.position.x
                    && corridorNearMilfSit.y == NpcObject.transform.position.y))
                {
                    targetNextCar = true;
                    targetPos = nextCar;
                    currentMission = "goToNextCar";
                    Debug.Log("currentMission is " + currentMission + "character pos" + NpcObject.transform.position + " target pos" + targetPos);
                    FlipCharacter();
                }
                else if (targetCorridorNearSit && targetNextCar
                    && !(nextCar.x == NpcObject.transform.position.x
                    && nextCar.y == NpcObject.transform.position.y))
                {
                    isMoving = true;
                }
                else if (targetCorridorNearSit && targetNextCar
                    && (nextCar.x == NpcObject.transform.position.x
                    && nextCar.y == NpcObject.transform.position.y))
                {
                    currentMission = "disappear";
                    npcAnimator.SetBool("hasToWalk", false);
                    Debug.Log("currentMission is " + currentMission + "character pos" + NpcObject.transform.position + " target pos" + targetPos);
                    gameObject.SetActive(false);
                }

            }
        }

    }
    private void FixedUpdate()
    {
        //after target position is known move the plumber to target
        if (isMoving)
        {
            // move player to the target place
            NpcObject.transform.position = Vector3.MoveTowards(NpcObject.transform.position, targetPos, speed);
            // is the player on target?
            if (NpcObject.transform.position.x == targetPos.x && NpcObject.transform.position.y == targetPos.y)
            {
                // when the player gets to the targer place set isMoving to false
                isMoving = false;
                //npcAnimator.SetBool("hasToWalk", false);
            }
        }
    }

    void FlipCharacter()
    {
        gameObject.transform.localScale = new Vector3(
            gameObject.transform.localScale.x * -1f,
            gameObject.transform.localScale.y,
            gameObject.transform.localScale.z);
    }

    IEnumerator MilfStandsUp()
    {
        yield return new WaitForSeconds(5.0f);
        isScriptActive = true;
        isMilfStanding = true;
        targetCorridorNearSit = true;
        currentMission = "goToCorridor";
        npcAnimator.SetBool("hasToWalk", true);
        Debug.Log("Milf is standing");
    }

    public void MilfGoesToRestourant()
    {
        npcAnimator.SetBool("hasToStandUp", true);
        StartCoroutine(MilfStandsUp());
        
    }
}
