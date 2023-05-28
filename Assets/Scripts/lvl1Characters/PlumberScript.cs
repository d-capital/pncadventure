using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlumberScript : DialogueTrigger
{
    public GameObject NpcObject;
    public bool isMoving;
    //-------------------------------------SCRIPTED SCENE:-----------------------------------------------//
    public bool isScriptActive; // indicate whether the script is running
    public bool targetWindow; //indicates that current script target is window
    public bool isPlumberStanding; // idicates wheter plumber is standing 
    public bool fromWindowToSit; //direct to sit
    public bool targetTreadmill; // direction to go and fix treadmill
    public bool fromTreadmillToSit; //direction after fixing treadmill to go back to sit
    public string currentMission; // tracks mission of the npc (where to go now)
    public bool hasFinishedFixing; // status changes when plumber finishes fixing something

    //--------------------------------------------------------------------------------------------------//
    public float speed;
    public Animator npcAnimator;
    //public string npcName;
    Vector2 targetPos;
    Vector2 window;
    Vector2 corridorNearSit;
    Vector2 corridorNearWc;
    Vector2 plumberSit;
    Vector2 treadmill;

    public bool isOnTarget;

    public bool xScaleWasChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        base.dialogue.name = GameObject.FindObjectOfType<LanguageManager>().getCorrectName("Plumber");
        npcAnimator.SetBool("isHappy", false);
    }

    // Update is called once per frame
    void Update()
    {
        //set all targets positions here
        corridorNearSit = GameObject.FindGameObjectWithTag("corridorAgainstPlumberSit").transform.position;
        corridorNearWc = GameObject.FindGameObjectWithTag("corridorAgainstWc").transform.position;
        plumberSit = GameObject.FindGameObjectWithTag("plumberSit").transform.position;
        window = GameObject.FindGameObjectWithTag("wcInside").transform.position;
        treadmill = GameObject.FindGameObjectWithTag("Treadmill").transform.position;

        //check if script is active
        if(isScriptActive)
        {
            if(targetWindow)
            {
                if(isPlumberStanding)
                {
                    if(!(corridorNearSit.x == NpcObject.transform.position.x && corridorNearSit.y == NpcObject.transform.position.y)
                     && currentMission == "")
                    {
                        //moveout
                        targetPos = corridorNearSit;
                        isMoving = true;
                        print("Target position BEFORE walking to wc : x - "+ targetPos.x + " y - "+ targetPos.y);
                        //CheckSpriteFlip();
                        currentMission = "goToCorridorNearWc";
                        //CheckSpriteFlip();
                    } 
                    else if ((corridorNearSit.x == NpcObject.transform.position.x && corridorNearSit.y == NpcObject.transform.position.y)
                        && currentMission == "goToCorridorNearWc")
                    {
                        if(!(corridorNearWc.x == NpcObject.transform.position.x && corridorNearWc.y == NpcObject.transform.position.y) 
                            && currentMission == "goToCorridorNearWc")
                        {
                            //go to wc
                            print("Target position BEFORE walking to wc : x - "+ targetPos.x + " y - "+ targetPos.y);
                            targetPos = corridorNearWc;
                            print("Target position AFTER walking to wc is activated : x - "+ targetPos.x + " y - "+ targetPos.y);
                            FlipCharacter();
                            isMoving = true;
                            currentMission = "getToWindow";
                        }
                    }
                    else if((corridorNearWc.x == NpcObject.transform.position.x && corridorNearWc.y == NpcObject.transform.position.y)
                        && currentMission == "getToWindow")
                    {
                        if(!(window.x == NpcObject.transform.position.x && window.y == NpcObject.transform.position.y) 
                            && currentMission == "getToWindow")
                        {
                            //go to window
                            targetPos = window;
                            //CheckSpriteFlip();
                            isMoving = true;
                            hasFinishedFixing = false;
                        }
                    }
                    else if((window.x == NpcObject.transform.position.x && window.y == NpcObject.transform.position.y) && hasFinishedFixing == false)
                    {
                        //play fix window animation
                        npcAnimator.SetBool("onTarget", true);
                        fromWindowToSit = true;
                        targetWindow = false;
                        GameObject.Find("wind").gameObject.SetActive(false);
                        currentMission = "moveOutOfWc";
                    }
                    
                }
            }
            else if(fromWindowToSit)
            {
                if(hasFinishedFixing)
                {
                    npcAnimator.SetBool("fixedSmthng", true);
                    npcAnimator.SetBool("onTarget", false);
                    if((window.x == NpcObject.transform.position.x && window.y == NpcObject.transform.position.y)
                        && currentMission == "moveOutOfWc")
                        {
                            targetPos = corridorNearWc;
                            isMoving = true;
                        }
                    else if((corridorNearWc.x == NpcObject.transform.position.x && corridorNearWc.y == NpcObject.transform.position.y)
                        && currentMission == "moveOutOfWc")
                        {
                            targetPos = corridorNearSit;
                            NpcObject.GetComponent<SpriteRenderer>().flipX = false;
                            FlipCharacter();
                            currentMission = "goBackToCorridorNearSit";
                            isMoving = true;
                        }
                    else if((corridorNearSit.x == NpcObject.transform.position.x && corridorNearSit.y == NpcObject.transform.position.y)
                        && currentMission == "goBackToCorridorNearSit")
                        {
                            targetPos = plumberSit;
                            NpcObject.GetComponent<SpriteRenderer>().flipX = true;
                            //CheckSpriteFlip();
                            currentMission = "goBackToPlumberSit";
                            isMoving = true;
                        }
                    else if((plumberSit.x == NpcObject.transform.position.x && plumberSit.y == NpcObject.transform.position.y)
                        && currentMission == "goBackToPlumberSit")
                        {
                            NpcObject.GetComponent<SpriteRenderer>().flipX = false;
                            npcAnimator.SetBool("isBackToSit", true);
                            isMoving = false;
                            npcAnimator.SetBool("readyToRelx",true);
                            npcAnimator.SetBool("hasToFixSmthing",false);
                            fromWindowToSit = false;
                            isScriptActive = false;
                        }
                    ///at the end set isScriptActive = false
                }
            }
            else if(targetTreadmill)
            {
                if(isPlumberStanding)
                {
                    if(!(corridorNearSit.x == NpcObject.transform.position.x && corridorNearSit.y == NpcObject.transform.position.y)
                     && currentMission == "")
                    {
                        //moveout
                        targetPos = corridorNearSit;
                        isMoving = true;
                        currentMission = "goToTreadmill";
                        CheckSpriteFlip();
                    }
                    else if((corridorNearSit.x == NpcObject.transform.position.x && corridorNearSit.y == NpcObject.transform.position.y)
                     && currentMission == "goToTreadmill")
                     {
                        targetPos = treadmill;
                        isMoving = true;
                        currentMission = "fixTreadmill";
                     }
                     else if((treadmill.x == NpcObject.transform.position.x && treadmill.y == NpcObject.transform.position.y)
                     && currentMission == "fixTreadmill")
                     {
                        //play fix treadmill animation
                        npcAnimator.SetBool("onTarget", true);
                        fromTreadmillToSit = true;
                        targetTreadmill = false;
                        currentMission = "goBackToCorridorNearSit";
                     }
                }
            }
            else if(fromTreadmillToSit)
            {
                if(hasFinishedFixing)
                {
                    npcAnimator.SetBool("fixedSmthng", true);
                    npcAnimator.SetBool("onTarget", false);
                    if((treadmill.x == NpcObject.transform.position.x && treadmill.y == NpcObject.transform.position.y)
                        && currentMission == "goBackToCorridorNearSit")
                        {
                            NpcObject.GetComponent<SpriteRenderer>().flipX = false;
                            FlipCharacter();
                            targetPos = corridorNearSit;
                            isMoving = true;
                        }
                    else if ((corridorNearSit.x == NpcObject.transform.position.x && corridorNearSit.y == NpcObject.transform.position.y)
                        && currentMission == "goBackToCorridorNearSit")
                        {
                            NpcObject.GetComponent<SpriteRenderer>().flipX = true;
                            targetPos = plumberSit;
                            //FlipCharacter();
                            isMoving = true;
                            currentMission = "sitDown";
                        }
                    else if((plumberSit.x == NpcObject.transform.position.x && plumberSit.y == NpcObject.transform.position.y)
                        && currentMission == "sitDown")
                        {
                            npcAnimator.SetBool("isBackToSit", true);
                            NpcObject.GetComponent<SpriteRenderer>().flipX = false;
                            isMoving = false;
                            npcAnimator.SetBool("readyToRelx",true);
                            npcAnimator.SetBool("hasToFixSmthing",false);
                            fromTreadmillToSit = false;
                            isScriptActive = false;
                        }
                }
                // at the end set isScriptActive = false
            }
        }
    }
    public void changePlumberAnimationToHappy()
	{
		npcAnimator.SetBool("isHappy", true);
	}

//--------------------------------SCRIPTED SCENE METHODS------------------------------------------------//
    //Start animation to stand up, post that it has ended through event in animation:
    public void plumberFixesWindow()
    {
        isScriptActive = true;
        targetWindow = true;
        targetTreadmill = false;
        npcAnimator.SetBool("hasToFixSmthing", true);
        npcAnimator.SetBool("hasToWalk", true);
        CheckSpriteFlip();
    }
    
    public void plumberFixesTreadmill()
    {
        isScriptActive = true;
        targetWindow = false;
        targetTreadmill = true;
        npcAnimator.SetBool("hasToFixSmthing", true);
        npcAnimator.SetBool("hasToWalk", true);
        CheckSpriteFlip();
    }
    //The following function should be called at the end of standing up animation:
    public void postStandingUpEnded ()
    {
        isPlumberStanding = true;
    }
    //Function below is called at the end of fixing animation:
    //don't forget to reset this variable to false when starting fix script
    public void postFinishedFixing ()
    {
        hasFinishedFixing = true;
    }
    // way to the WC:
    public void whereToGo()
    {
        //reachedDestination = false;
        if(npcAnimator.GetBool("hasToFixSmthing") == true 
        && npcAnimator.GetBool("hasToWalk") == true)
        {
            executeMovingOutToCorridor();
            StartCoroutine(executeGoingToWindow());
        }
    }
    
    public void executeMovingOutToCorridor()
    {
        isMoving = true;
        corridorNearSit = GameObject.FindGameObjectWithTag("corridorAgainstPlumberSit").transform.position;
        targetPos = corridorNearSit;
        CheckSpriteFlip();
    }
    IEnumerator executeGoingToWindow()
    {
        yield return new WaitForSeconds(1.0f);
        corridorNearWc = GameObject.FindGameObjectWithTag("corridorAgainstWc").transform.position;
        isMoving = true;
        targetPos = corridorNearWc;
        //window = GameObject.FindGameObjectWithTag("wcInside").transform.position;
        //isMoving = true;
        //targetPos = window;
        //CheckSpriteFlip();
    }
    IEnumerator executeMovingToWindow()
    {
        yield return new WaitForSeconds(1.0f);
        window = GameObject.FindGameObjectWithTag("wcInside").transform.position;
        isMoving = true;
        targetPos = window;
        StartCoroutine(startFixing());
    }

    IEnumerator startFixing()
    {
        yield return new WaitForSeconds(1.0f);
        npcAnimator.SetBool("fix",true);
    }
    //way back to sit:

    private void FixedUpdate()
    {
        //after target position is known move the plumber to target
        if(isMoving)
        {
            // move player to the target place
            NpcObject.transform.position = Vector3.MoveTowards(NpcObject.transform.position, targetPos, speed);
            // is the player on target?
            if(NpcObject.transform.position.x == targetPos.x && NpcObject.transform.position.y == targetPos.y)
            {
                // when the player gets to the targer place set isMoving to false
                isMoving = false;
                //npcAnimator.SetBool("hasToWalk", false);
            }
        }
    }
    

    /*void CheckSpriteFlip()
    {
        if(NpcObject.transform.position.x > targetPos.x)
        {
            //turn left
            NpcObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            //turn right
            NpcObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }*/

    void CheckSpriteFlip()
    {
        if (gameObject.transform.position.x > targetPos.x && !xScaleWasChanged)
        {
            //turn left
            //player.GetComponent<SpriteRenderer>().flipX = true;
            gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x * -1f,
                gameObject.transform.localScale.y,
                gameObject.transform.localScale.z);
            xScaleWasChanged = true;
        }
        else if (gameObject.transform.position.x < targetPos.x && xScaleWasChanged)
        {
            //turn right
            //player.GetComponent<SpriteRenderer>().flipX = false;
            gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x * -1f,
                gameObject.transform.localScale.y,
                gameObject.transform.localScale.z);
            xScaleWasChanged = false;
        }
    }

    void FlipCharacter()
    {
        gameObject.transform.localScale = new Vector3(
            gameObject.transform.localScale.x * -1f,
            gameObject.transform.localScale.y,
            gameObject.transform.localScale.z);
    }
}
