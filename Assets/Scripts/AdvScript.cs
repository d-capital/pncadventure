using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class AdvScript : MonoBehaviour
{

    public Vector3 mousePos;
    float distance;
    public Vector2 playerPosition;
    public Camera mainCamera;
    public Vector3 mousePosWorld;
    public Vector2 mousePosWorld2D;
    RaycastHit2D hit;
    public GameObject player;
    public Vector2 targetPos;
    public float speed;
    public bool isMoving;

    public bool key = false;
    //--------Level 1 Attributes--------//
    public bool helpedOldWoman = false;
    public bool talkedToOldWoman = false;
    public bool windowClosed = false;
    public bool hasVodka = false;
    public bool treadmillFixed = false;
    public bool hasScrewKey = false;
    public bool talkedToHunks = false;
    public bool clickedOnWindow = false;
    public bool vodkaGivenToPlumber = false;
    public int clicksOnOldWomanBed = 0;
    public int clickedOnHunks = 0;
    public bool dialougeBoxOpen = false;
    public bool informedHunksTreadmillIsFixed = false;
    //--------Level 2 Attributes-------------------------------//
    //--------cardsMan-----------------//
    public bool talkedToCardsMan = false;
    public bool wonCardsMan = false;
    public bool lostToCardsMan1stTime = false;
    public bool lostToCardsMan2ndTime = false;
    public bool rejectedToPlay2ndTime = false;
    public bool rejectedToPlay3rdTime = false;
    //---------fatMan------------------//
    public bool hasMoney = false;
    public bool boughtPirogi = false;
    public bool is1stRiddleSolved = false;
    public bool is1stRiddleAttempted = false;
    public bool is2ndRiddleSolved = false;
    public bool is2ndRiddleAttempted = false;
    public bool wasHintHelpful = false;
    public bool sharedPirogyWithYou = false;

    //---------milf--------------------//
    public bool milfRightAnswer1 = false;
    public bool milfAttempt1Done = false;
    public bool milfRightAnswer2 = false;
    public bool milfAttempt2Done = false;
    public bool milfRightAnswer3 = false;
    public bool milfAttempt3Done = false;

    //---------emergencyButton---------//
    public bool isButtonFixed = false;
    public bool isButtonPushed = false;
    public bool wasCrimeReported = false;
    public bool wireInTopSpot = false;
    public bool wireInBottomSpot = false;
    public bool wireInMiddleSpot = false;
    public bool allDone = false;
    //--------------------------------------------------------//
    IList dialogBoxesSearchResults;
    DialogueBoxHandler dialogueBox;
    DialogueTrigger currentDialogueTrigger;
    //-----------------Dialogues Level 1-------------//
    DialogueTrigger oldWomanDialogue;
    DialogueTrigger oldWomanBedDialogue;
    DialogueTrigger plumberDialogue;
    DialogueTrigger hunkDialogue;
    DialogueTrigger hunk1Dialogue;
    DialogueTrigger hunk2Dialogue;
    //-----------------------------------------------//

    //-----------------Dialogues Level 2-------------//
    DialogueTrigger semenDialogue;
    DialogueTrigger cardsManDialogue;
    DialogueTrigger fatManDialogue;
    DialogueTrigger milfDialogue;
    DialogueTrigger emergencyButtonDialogue;
    DialogueTrigger cardsManBedDialogue;
    DialogueTrigger milfBedDialogue;
    //----------------------------------------------//

    public string currentDialoguePartner;

    public Animator animator;
    public AudioSource collectSound;

    public bool introShown = false;

    //public TextMeshProUGUI newInfoText;

    // Use this for initialization if something needs to be defined before the game starts
    void Start()
    {
        GetCurrentLevel();
    }

    void OnLevelFinishedLoading()
    {
        if (GetCurrentLevel() == "Level 1")
        {
            ShowDialogueBox();
            currentDialoguePartner = "SemenLvl1Intro";
            semenDialogue = GameObject.Find("Player").GetComponent<DialogueTrigger>();
            semenDialogue.TriggerDialogue("SemenLvl1Intro");
        }
        else if (GetCurrentLevel() == "Level 2")
        {
            ShowDialogueBox();
            currentDialoguePartner = "SemenLvl2Intro";
            semenDialogue = GameObject.Find("Player").GetComponent<DialogueTrigger>();
            semenDialogue.TriggerDialogue("SemenLvl2Intro");
        }
    }
    public void ShowOutro()
    {
        if (GetCurrentLevel() == "Level 1")
        {
            ShowDialogueBox();
            currentDialoguePartner = "SemenLvl1Outro";
            semenDialogue = GameObject.Find("Player").GetComponent<DialogueTrigger>();
            semenDialogue.TriggerDialogue("SemenLvl1Outro");
        }
        else if (GetCurrentLevel() == "Level 2")
        {
            ShowDialogueBox();
            currentDialoguePartner = "SemenLvl2Outro";
            semenDialogue = GameObject.Find("Player").GetComponent<DialogueTrigger>();
            semenDialogue.TriggerDialogue("SemenLvl2Outro");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > 0.5 && introShown == false)
        {
            OnLevelFinishedLoading();
            introShown = true;
        }
        if (Input.GetMouseButtonDown(0))
        {

            mousePos = Input.mousePosition;
            mousePosWorld = mainCamera.ScreenToWorldPoint(mousePos);
            // Change Vector3 into Vector 2
            mousePosWorld2D = new Vector2(mousePosWorld.x, mousePosWorld.y);
            // Raycast2D => hit saved
            hit = Physics2D.Raycast(mousePosWorld2D, Vector2.zero);

            // check if hit has a collider
            if (hit.collider != null && !EventSystem.current.IsPointerOverGameObject() && PauseMenu.isGamePaused == false)
            {
                print("Clicked on the object with the name: " + hit.collider.gameObject.tag);
                if(hit.collider.gameObject.tag == "ScrewKey")
                {
                    // key was hit, hide the key, save it's state to the object
                    hit.collider.gameObject.SetActive(false);
                    var collectibleItem = hit.collider.gameObject.GetComponent<PickupScript>();
                    collectibleItem.onClick();
                    //newInfoText.ClearMesh();
                    string newInfoText = "Что это? Гаечный ключ? Штука нужная, может пригодится.";
                    ShowInfoText(newInfoText);
                    key = true;
                    hasScrewKey = true;
                    collectSound.Play();
                    // save the number of the objects of the same type
                }
                else if(hit.collider.gameObject.tag == "InventoryElements")
                {
                    // key was hit, hide the key, save it's state to the object
                    hit.collider.gameObject.GetComponent<InventoryUI>().OpenCloseInventory();
                }
                else if(hit.collider.gameObject.tag == "Vodka")
                {
                    // key was hit, hide the key, save it's state to the object
                    hit.collider.gameObject.SetActive(false);
                    var collectibleItem = hit.collider.gameObject.GetComponent<PickupScript>();
                    collectibleItem.onClick();
                    //newInfoText.ClearMesh();
                    string newInfoText = "Xo6a, водка!";
                    ShowInfoText(newInfoText);
                    //key = true;
                    //hasScrewKey = true;
                    collectSound.Play();
                    // save the number of the objects of the same type
                }
                else if (hit.collider.gameObject.tag == "OldWoman")
                {
                    //start the dialogue only if the player is close enough:
                    currentDialoguePartner = hit.collider.gameObject.tag;
                    playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
                    distance = Vector2.Distance(playerPosition, mousePosWorld2D);
                    if(distance <= 3f)
                    {
                        ShowDialogueBox();
                        oldWomanDialogue = GameObject.FindGameObjectWithTag("OldWoman").GetComponent<DialogueTrigger>();
                        oldWomanDialogue.TriggerDialogue("OldWoman");
                    }
                 
                }
                else if(hit.collider.gameObject.tag == "OldWomansBed")
                {
                    currentDialoguePartner = hit.collider.gameObject.tag;
                    if(windowClosed == false && helpedOldWoman == true)
                    {
                        clicksOnOldWomanBed = clicksOnOldWomanBed +1;
                    };
                    ShowDialogueBox();
                    oldWomanBedDialogue = GameObject.FindGameObjectWithTag("OldWomansBed").GetComponent<DialogueTrigger>();
                    oldWomanBedDialogue.TriggerDialogueOldWomansBed(currentDialoguePartner);
                }
                else if(hit.collider.gameObject.tag == "Plumber")
                {
                    var plumberScript = GameObject.FindGameObjectWithTag("Plumber").GetComponent<PlumberScript>();
                    if(!plumberScript.isScriptActive)
                    {
                        currentDialoguePartner = hit.collider.gameObject.tag;
                        ShowDialogueBox();
                        plumberDialogue = GameObject.FindGameObjectWithTag("Plumber").GetComponent<DialogueTrigger>();
                        plumberDialogue.TriggerDialoguePlumber("Plumber");
                    }

                }
                else if(hit.collider.gameObject.tag == "Hunk" || hit.collider.gameObject.tag == "Hunk2" || hit.collider.gameObject.tag == "Hunk3")
                {
                    currentDialoguePartner = hit.collider.gameObject.tag;
                    if(treadmillFixed == false && windowClosed == true)
                    {
                        clickedOnHunks = clickedOnHunks + 1;
                    }
                    ShowDialogueBox();
                    hunkDialogue = GameObject.FindGameObjectWithTag(currentDialoguePartner).GetComponent<DialogueTrigger>();
                    hunkDialogue.TriggerDialogue(currentDialoguePartner);
                }
                else if (hit.collider.gameObject.tag == "Ground" && dialougeBoxOpen == false)
                {
                    // if user cliced on the scene ground, move the player
                    targetPos = hit.point;
                    // set isMoving true, so that the player moves
                    isMoving = true;
                    animator.SetBool("IsMoving", true);
                    //check if we need to flip the sprite of the player according to movement direction
                    CheckSpriteFlip();
                }
                else if(hit.collider.gameObject.tag == "Window")
                {
                    //show hint
                    string newInfoText;
                    if (windowClosed == false)
                    {
                        newInfoText = "Сломалась, так просто не закрыть.";
                        ShowInfoText(newInfoText);
                    } 
                    else
                    {
                        newInfoText = "Окно закрыто, не дует.";
                        ShowInfoText(newInfoText);
                    }
                    
                }
                //----------------Level 2 interactions------------------------//
                else if(hit.collider.gameObject.GetComponent<cardsManScript>())
                {
                    currentDialoguePartner = "cardsMan";
                    cardsManDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                    ShowDialogueBox();
                    cardsManDialogue.TriggerDialogue(currentDialoguePartner);
                }
                else if(hit.collider.gameObject.GetComponent<fatManScript>())
                {
                    print("found fatMan");
                    currentDialoguePartner = "fatMan";
                    fatManDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                    ShowDialogueBox();
                    fatManDialogue.TriggerDialogue(currentDialoguePartner);
                }
                else if(hit.collider.gameObject.GetComponent<milfScript>())
                {
                    print("found milf");
                    currentDialoguePartner = "milf";
                    milfDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                    ShowDialogueBox();
                    milfDialogue.TriggerDialogue(currentDialoguePartner);
                }
                else if(hit.collider.gameObject.GetComponent<emergencyButtonScript>())
                {
                    print("found emergencyButton");
                    currentDialoguePartner = "emergencyButton";
                    emergencyButtonDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                    ShowDialogueBox();
                    emergencyButtonDialogue.TriggerDialogue(currentDialoguePartner);
                }
                else if (hit.collider.gameObject.GetComponent<cardsManBedScript>())
                {
                    if (wasCrimeReported)
                    {
                        currentDialoguePartner = "cardsManBed";
                        cardsManBedDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                        ShowDialogueBox();
                        cardsManBedDialogue.TriggerDialogue(currentDialoguePartner);
                    }
                }
                else if (hit.collider.gameObject.GetComponent<milfBedScript>())
                {
                    if (milfRightAnswer3)
                    {
                        currentDialoguePartner = "milfBed";
                        milfBedDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                        ShowDialogueBox();
                        milfBedDialogue.TriggerDialogue(currentDialoguePartner);
                    }
                }
            }
            else
            {
                print("No collider hit!");
            }
        }
    }

    public void Answer(int incomingResponseIndex)
    {
        MapDialogueTrigger(currentDialoguePartner).Answer(currentDialoguePartner, incomingResponseIndex);
    }
    public void ShowDialogueBox()
    {
        //GameObject canvasObj = GameObject.FindGameObjectWithTag("Canvas");
        //Transform[] trs = canvasObj.GetComponentsInChildren<Transform>(true);
        //foreach(Transform t in trs)
        //{
          //  if(t.tag == "DialogueBox")
            //{
              //  t.gameObject.SetActive(true);
            //}
        //}
        dialogBoxesSearchResults = Resources.FindObjectsOfTypeAll<DialogueBoxHandler>();
        foreach (DialogueBoxHandler i in dialogBoxesSearchResults)
        {
            i.ShowDialogue();
        }
        dialougeBoxOpen = true;
    }
    public void ShowInfoText(string InfoText)
    {
        var infoTextSearchResults = Resources.FindObjectsOfTypeAll(typeof(InfoText));
        foreach (InfoText i in infoTextSearchResults)
        {
            i.ShowInfoText(InfoText);
        }
    }
    private void FixedUpdate()
    {
        // check if player is moving
        if(isMoving)
        {
            // move player to the target place
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPos, speed);
            // is the player on target?
            if(player.transform.position.x == targetPos.x && player.transform.position.y == targetPos.y)
            {
                // when the player gets to the targer place set isMoving to false
                isMoving = false;
                animator.SetBool("IsMoving", false);
                Debug.Log(player.transform.position.x);
            }
        }
    }

    void CheckSpriteFlip()
    {
        if(player.transform.position.x > targetPos.x)
        {
            //turn left
            player.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            //turn right
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public DialogueTrigger MapDialogueTrigger(string currentDialoguePartner)
    {
        try
        {
            currentDialogueTrigger = GameObject.FindGameObjectWithTag(currentDialoguePartner).GetComponent<DialogueTrigger>();
        }
        catch (UnityException)
        {
            if(currentDialoguePartner == "cardsMan")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<cardsManScript>().GetComponent<DialogueTrigger>();
            }
            else if(currentDialoguePartner == "fatMan")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<fatManScript>().GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "milf")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<milfScript>().GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "milfBed")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<milfBedScript>().GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "cardsManBed")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<cardsManBedScript>().GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "emergencyButton")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<emergencyButtonScript>().GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "SemenLvl2Intro")
            {
                currentDialogueTrigger = GameObject.Find("Player").GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "SemenLvl1Intro")
            {
                currentDialogueTrigger = GameObject.Find("Player").GetComponent<DialogueTrigger>();
            }
        }
        return currentDialogueTrigger;
    }
    public void LoadNextLevel(PlayerData playerData)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        SaveSystem.SavePlayer(playerData);
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public string GetCurrentLevel()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        print(currentLevel);
        return currentLevel;
    }

}