using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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

    public bool xScaleWasChanged = false;

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

    //--------Level 3 Attributes-------------------------------//
    //-------Shaman-----------------------//
    public bool initialSuccess = false;
    public bool initialFailed = false;
    public bool secondSuccess = false;
    public bool secondFailed = false;
    public bool potionGiven = false;
    //-------meMom------------------------//
    public bool shahtarsHigh = false;
    public bool informedMeMomShahtarsAreHigh = false;
    public bool gotBathItems = false;
    public bool semenTalkedToMeMom = false;
    //-------Shahtars---------------------//
    public bool shahtarInitialFailed = false;
    public bool timeForRound2 = false;
    public bool failedRound2 = false;
    public bool timeForRound3 = false;
    public bool gotCrowbar = false;
    public bool shahtarsGotPotion = false;
    //-------shahtarsBed------------------//
    public bool fuckedOff1 = false;
    public bool bathed = false;
    //--------------------------------------------------------//

    //--------Level 4 Attributes-------------------------------//
    //-------Major----------------------------//
    public bool majorGirlHasShuba = false;
    public bool gotQuestFromMajor = false;
    //-------MajorGirl------------------------//
    public bool wrongAnswer = false;
    public bool girlReasked = false;
    public bool girlAskedForShuba = false;
    public bool rightAnswer = false;
    public bool gotShubaFromHunter = false;
    public bool majorInformed = false;
    //-------Hunter---------------------------//
    public bool firstTalk = true;
    public bool hunterAsksForTea = false;
    public bool hasAspirin = false; // need a way to indentify the state of inventory
    public bool hasTea = false; // need a way to indentify the state of inventory
    public bool hunterReasksTea = false;
    public bool teaGivenToHunter = false;
    public bool aspirinGivenToHunter = false;

    //-------GrandMaster----------------------//
    public bool grandmasterAsksForTea = false;
    public bool chessAnswerOne = false;
    public bool chessAnswerTwo = false;
    public bool chessAnswerThree = false;
    public bool grandMasterThanked = false;
    public bool chessWrongAnswer = false;
    //-------CabCrew----------------------//
    public bool semenAskedForPayment = false;
    public bool rightPaymentRecipient = false;
    public bool wrongRecipient = false;
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
    DialogueTrigger npcDialogue;
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
    
    //-----------------Dialogues Level 3-------------//
    DialogueTrigger shamanDialogue;
    DialogueTrigger meMomDialogue;
    DialogueTrigger shatarDialogue;
    DialogueTrigger shahtarBedDialogue;
    //----------------------------------------------//

    //-----------------Dialogues Level 4-------------//
    DialogueTrigger majorDialogue;
    DialogueTrigger majorGirlDialogue;
    DialogueTrigger grandMasterDialogue;
    DialogueTrigger hunterDialogue;
    DialogueTrigger cabCrewDialogue;
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
        PlayerData playerData = SaveSystem.LoadPlayer();
        if (playerData.hasAspirin)
        {
            GameObject.FindObjectOfType<DialogueManager>().GetComponent<DialogueManager>().GetItemToInventory("aspirinButton");
        }
        if (GetCurrentLevel() == "Level 1")
        {
            ShowDialogueBox();
            currentDialoguePartner = "SemenLvl1Intro";
            semenDialogue = GameObject.Find("Player").GetComponent<DialogueTrigger>();
            semenDialogue.dialogue.name = GameObject.FindObjectOfType<LanguageManager>().getCorrectName("Semen");
            semenDialogue.TriggerDialogue("SemenLvl1Intro");
        }
        else if (GetCurrentLevel() == "Level 2")
        {
            ShowDialogueBox();
            currentDialoguePartner = "SemenLvl2Intro";
            semenDialogue = GameObject.Find("Player").GetComponent<DialogueTrigger>();
            semenDialogue.dialogue.name = GameObject.FindObjectOfType<LanguageManager>().getCorrectName("Semen");
            semenDialogue.TriggerDialogue("SemenLvl2Intro");
        }
        else if (GetCurrentLevel() == "Level 3")
        {
            ShowDialogueBox();
            currentDialoguePartner = "SemenLvl3Intro";
            semenDialogue = GameObject.Find("Player").GetComponent<DialogueTrigger>();
            semenDialogue.dialogue.name = GameObject.FindObjectOfType<LanguageManager>().getCorrectName("Semen");
            semenDialogue.TriggerDialogue("SemenLvl3Intro");    
        }
        else if (GetCurrentLevel() == "Level 4")
        {
            ShowDialogueBox();
            currentDialoguePartner = "SemenLvl4Intro";
            semenDialogue = GameObject.Find("Player").GetComponent<DialogueTrigger>();
            semenDialogue.dialogue.name = GameObject.FindObjectOfType<LanguageManager>().getCorrectName("Semen");
            semenDialogue.TriggerDialogue("SemenLvl4Intro");
        }

    }
    public void ShowOutro()
    {
        if (GetCurrentLevel() == "Level 1")
        {
            ShowDialogueBox();
            currentDialoguePartner = "SemenLvl1Outro";
            semenDialogue = GameObject.Find("Player").GetComponent<DialogueTrigger>();
            semenDialogue.dialogue.name = GameObject.FindObjectOfType<LanguageManager>().getCorrectName("Semen");
            semenDialogue.TriggerDialogue("SemenLvl1Outro");
        }
        else if (GetCurrentLevel() == "Level 2")
        {
            ShowDialogueBox();
            currentDialoguePartner = "SemenLvl2Outro";
            semenDialogue = GameObject.Find("Player").GetComponent<DialogueTrigger>();
            semenDialogue.dialogue.name = GameObject.FindObjectOfType<LanguageManager>().getCorrectName("Semen");
            semenDialogue.TriggerDialogue("SemenLvl2Outro");
        }
        else if (GetCurrentLevel() == "Level 3")
        {
            ShowDialogueBox();
            currentDialoguePartner = "SemenLvl3Outro";
            semenDialogue = GameObject.Find("Player").GetComponent<DialogueTrigger>();
            semenDialogue.dialogue.name = GameObject.FindObjectOfType<LanguageManager>().getCorrectName("Semen");
            semenDialogue.TriggerDialogue("SemenLvl3Outro");
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
            if (hit.collider != null && !EventSystem.current.IsPointerOverGameObject() 
                && (GameObject.FindObjectOfType<PauseMenu>().isGamePaused == false 
                && GameObject.FindObjectOfType<TasksButton>().areTasksOpen == false))
            {
                print("Clicked on the object with the name: " + hit.collider.gameObject.tag);
                if(hit.collider.gameObject.tag == "ScrewKey")
                {
                    // key was hit, hide the key, save it's state to the object
                    hit.collider.gameObject.SetActive(false);
                    var collectibleItem = hit.collider.gameObject.GetComponent<PickupScript>();
                    collectibleItem.onClick();
                    //newInfoText.ClearMesh();
                    string newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "ScrewKey");
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
                    string newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "Vodka");
                    ShowInfoText(newInfoText);
                    //key = true;
                    //hasScrewKey = true;
                    collectSound.Play();
                    // save the number of the objects of the same type
                }
                else if (hit.collider.gameObject.tag == "coins")
                {
                    // key was hit, hide the key, save it's state to the object
                    hit.collider.gameObject.SetActive(false);
                    var collectibleItem = hit.collider.gameObject.GetComponent<PickupScript>();
                    collectibleItem.onClick();
                    //newInfoText.ClearMesh();
                    string newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "coins");
                    ShowInfoText(newInfoText);
                    //key = true;
                    //hasScrewKey = true;
                    collectSound.Play();
                    // save the number of the objects of the same type
                }
                else if (hit.collider.gameObject.GetComponent<npc>())
                {
                    print("found npc");
                    currentDialoguePartner = "npc";
                    npcDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                    ShowDialogueBox();
                    npcDialogue.TriggerDialogue(currentDialoguePartner);
                }
                else if (hit.collider.gameObject.tag == "OldWoman")
                {
                    //start the dialogue only if the player is close enough:
                    currentDialoguePartner = hit.collider.gameObject.tag;
                    ShowDialogueBox();
                    oldWomanDialogue = GameObject.FindGameObjectWithTag("OldWoman").GetComponent<DialogueTrigger>();
                    oldWomanDialogue.TriggerDialogue("OldWoman");

                 
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
                        newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "WindowBroken");
                        ShowInfoText(newInfoText);
                    } 
                    else
                    {
                        newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "WindowFixed");
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
                    var milfScript = GameObject.FindObjectOfType<milfScript>().GetComponent<milfScript>();
                    if (!milfScript.isScriptActive)
                    {
                        currentDialoguePartner = "milf";
                        milfDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                        ShowDialogueBox();
                        milfDialogue.TriggerDialogue(currentDialoguePartner);
                    }
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
                //----------------Level 3 interactions------------------------//
                else if (hit.collider.gameObject.GetComponent<shamanScript>())
                {
                    currentDialoguePartner = "Shaman";
                    shamanDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                    ShowDialogueBox();
                    shamanDialogue.TriggerDialogue(currentDialoguePartner);
                }
                else if (hit.collider.gameObject.GetComponent<meMomScript>())
                {
                    currentDialoguePartner = "meMom";
                    meMomDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                    ShowDialogueBox();
                    meMomDialogue.TriggerDialogue(currentDialoguePartner);
                }
                else if (hit.collider.gameObject.GetComponent<shahtarScript>())
                {
                    currentDialoguePartner = "Shahtar";
                    shatarDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                    ShowDialogueBox();
                    shatarDialogue.TriggerDialogue(currentDialoguePartner);
                }
                else if (hit.collider.gameObject.GetComponent<shahtarBedScript>())
                {
                    if (gotCrowbar)
                    {
                        currentDialoguePartner = "shahtarBed";
                        shahtarBedDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                        ShowDialogueBox();
                        shahtarBedDialogue.TriggerDialogue(currentDialoguePartner);
                    }
                }
                else if (hit.collider.gameObject.GetComponent<toiletDoor>())
                {
                    //show hint
                    string newInfoText;
                    if (!gotCrowbar)
                    {
                        newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "ToiletDoorNoCrowbar");
                        ShowInfoText(newInfoText);
                    }
                    else
                    {
                        newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "ToiletDoorHasCrowbar");
                        ShowInfoText(newInfoText);
                    }
                }
                else if (hit.collider.gameObject.GetComponent<sink>())
                {
                    //show hint
                    string newInfoText;
                    if (!gotBathItems)
                    {
                        newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "sinkNoBathItems");
                        ShowInfoText(newInfoText);
                    }
                    else
                    {
                        newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "sinkHasBathItems");
                        ShowInfoText(newInfoText);
                    }
                }
                else if (hit.collider.gameObject.tag == "aspirin")
                {
                    hit.collider.gameObject.SetActive(false);
                    var collectibleItem = hit.collider.gameObject.GetComponent<PickupScript>();
                    collectibleItem.onClick();
                    string newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "aspirin");
                    ShowInfoText(newInfoText);
                    collectSound.Play();
                }
                else if (hit.collider.gameObject.name.Contains("crowBar"))
                {
                    hit.collider.gameObject.SetActive(false);
                    var collectibleItem = hit.collider.gameObject.GetComponent<PickupScript>();
                    collectibleItem.onClick();
                    string newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "crowBar");
                    ShowInfoText(newInfoText);
                    collectSound.Play();
                }
                else if (hit.collider.gameObject.name.Contains("potion"))
                {
                    hit.collider.gameObject.SetActive(false);
                    var collectibleItem = hit.collider.gameObject.GetComponent<PickupScript>();
                    collectibleItem.onClick();
                    string newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "potion");
                    ShowInfoText(newInfoText);
                    collectSound.Play();
                }
                else if (hit.collider.gameObject.name.Contains("bathItems"))
                {
                    hit.collider.gameObject.SetActive(false);
                    var collectibleItem = hit.collider.gameObject.GetComponent<PickupScript>();
                    collectibleItem.onClick();
                    string newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "bathItems");
                    ShowInfoText(newInfoText);
                    collectSound.Play();
                }
                //----------------Level 4 interactions------------------------//
                else if (hit.collider.gameObject.GetComponent<majorScript>())
                {
                    currentDialoguePartner = "Major";
                    majorDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                    ShowDialogueBox();
                    majorDialogue.TriggerDialogue(currentDialoguePartner);
                }
                else if (hit.collider.gameObject.GetComponent<majorGirlScript>())
                {
                    currentDialoguePartner = "MajorGirl";
                    majorGirlDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                    ShowDialogueBox();
                    majorGirlDialogue.TriggerDialogue(currentDialoguePartner);
                }
                else if (hit.collider.gameObject.GetComponent<hunterScript>())
                {
                    currentDialoguePartner = "Hunter";
                    hunterDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                    ShowDialogueBox();
                    hunterDialogue.TriggerDialogue(currentDialoguePartner);
                }
                else if (hit.collider.gameObject.GetComponent<grandMasterScript>())
                {
                    currentDialoguePartner = "GrandMaster";
                    grandMasterDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                    ShowDialogueBox();
                    grandMasterDialogue.TriggerDialogue(currentDialoguePartner);
                }
                else if (hit.collider.gameObject.GetComponent<cabCrewScript>())
                {
                    currentDialoguePartner = "CabCrew";
                    cabCrewDialogue = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                    ShowDialogueBox();
                    cabCrewDialogue.TriggerDialogue(currentDialoguePartner);
                }
                else if (hit.collider.gameObject.name.Contains("boyaryshnik"))
                {
                    hit.collider.gameObject.SetActive(false);
                    var collectibleItem = hit.collider.gameObject.GetComponent<PickupScript>();
                    collectibleItem.onClick();
                    string newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "boyaryshnik");
                    ShowInfoText(newInfoText);
                    collectSound.Play();
                }
                else if (hit.collider.gameObject.name.Contains("glass"))
                {
                    hit.collider.gameObject.SetActive(false);
                    var collectibleItem = hit.collider.gameObject.GetComponent<PickupScript>();
                    collectibleItem.onClick();
                    string newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "glass");
                    ShowInfoText(newInfoText);
                    collectSound.Play();
                }
                else if (hit.collider.gameObject.name.Contains("shuba"))
                {
                    hit.collider.gameObject.SetActive(false);
                    var collectibleItem = hit.collider.gameObject.GetComponent<PickupScript>();
                    collectibleItem.onClick();
                    string newInfoText = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("infoTexts", "shuba");
                    ShowInfoText(newInfoText);
                    collectSound.Play();
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
        if (!GameObject.FindObjectOfType<PauseMenu>().isGamePaused && !GameObject.FindObjectOfType<GameOver>().isGameOverScreenShown)
        {
            MapDialogueTrigger(currentDialoguePartner).Answer(currentDialoguePartner, incomingResponseIndex);
        }
    }
    public void ShowDialogueBox()
    {
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
        if(player.transform.position.x > targetPos.x && !xScaleWasChanged)
        {
            //turn left
            //player.GetComponent<SpriteRenderer>().flipX = true;
            player.transform.localScale = new Vector3(
                player.transform.localScale.x * -1f,
                player.transform.localScale.y,
                player.transform.localScale.z);
            xScaleWasChanged = true;
        }
        else if (player.transform.position.x < targetPos.x && xScaleWasChanged)
        {
            //turn right
            //player.GetComponent<SpriteRenderer>().flipX = false;
            player.transform.localScale = new Vector3(
                player.transform.localScale.x * -1f,
                player.transform.localScale.y,
                player.transform.localScale.z);
            xScaleWasChanged = false;
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
            //LVL3
            else if (currentDialoguePartner == "SemenLvl3Intro")
            {
                currentDialogueTrigger = GameObject.Find("Player").GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "meMom")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<meMomScript>().GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "Shaman")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<shamanScript>().GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "shahtarBed")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<shahtarBedScript>().GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "Shahtar")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<shahtarScript>().GetComponent<DialogueTrigger>();
            }
            //LVL4
            else if (currentDialoguePartner == "SemenLvl4Intro")
            {
                currentDialogueTrigger = GameObject.Find("Player").GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "Major")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<majorScript>().GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "MajorGirl")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<majorGirlScript>().GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "Hunter")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<hunterScript>().GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "GrandMaster")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<grandMasterScript>().GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "CabCrew")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<cabCrewScript>().GetComponent<DialogueTrigger>();
            }
            else if (currentDialoguePartner == "npc")
            {
                currentDialogueTrigger = GameObject.FindObjectOfType<npc>().GetComponent<DialogueTrigger>();
            }
        }
        return currentDialogueTrigger;
    }
    public void LoadNextLevel(PlayerData playerData)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        SaveSystem.SavePlayer(playerData);
    }

    public void LoadDistinctLevel(PlayerData playerData, int lvlIndex)
    {
        SceneManager.LoadScene(lvlIndex);
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