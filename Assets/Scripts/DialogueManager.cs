using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class DialogueManager : MonoBehaviour
{
    int curResponseTracker;
    int futResponseTracker;
    string currDialogueActor;
    public Text nameText;
    //public Text nameText2;
    public Text dialogText;
    //public Text dialogueText2;

    //public TextMeshProUGUI response;

    //public GameObject[] responseButtons;
    private string[] sentences;
    
    //state calc
    private bool hasVodka;
    public bool helpedOldWoman;
    public bool talkedToOldWoman;
    public bool windowClosed;
    public bool treadmillFixed;
    public bool hasScrewKey;
    public bool talkedToHunks;
    public bool clickedOnWindow;
    public bool vodkaGivenToPlumber;
    public int clicksOnOldWomanBed;
    public int clickedOnHunks;
    public bool informedHunksTreadmillIsFixed;
    private string lvl1State;
    private string lvl2State;

    PlayerData playerData = new PlayerData();

    //Lines and Answers model
    [System.Serializable]
    public class LinesAnswer
    {
        public string name { get; set; }
        public string line { get; set; }
        public List<Response> responses { get; set; }
    }

    [System.Serializable]
    public class Response
    {
        public string id { get; set; }
        public string response { get; set; }
    }

    [System.Serializable]
    public class LinesAnswersList
    {
        public List<LinesAnswer> LinesAnswers { get; set; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //sentences = new Queue<string>();
    }
    //Old Woman
    public void StartDialogue (Dialogue dialogue, string dialogueActor)
    {
        ClearLinesAnswers(dialogue);
        currDialogueActor = dialogueActor;
        curResponseTracker = 0;
        nameText.text = dialogue.name;
        //nameText2.text = dialogue.name;
        var currentLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().GetCurrentLevel();
        if(currentLevel == "Level 1")
        {
            lvl1stateCalculator(currDialogueActor,dialogue);
        }
        else if(currentLevel == "Level 2")
        {
            lvl2stateCalculator(currDialogueActor, dialogue);
        }
        
    }
    public void StartDialogueWithOldWomansBed(Dialogue dialogue, string dialogueActor)
    {
        ClearLinesAnswers(dialogue);
        currDialogueActor = dialogueActor;
        curResponseTracker = 0;
        nameText.text = dialogue.name;
        //nameText2.text = dialogue.name;
        lvl1stateCalculator(currDialogueActor,dialogue);
    }
    public void StartDialogueWithPlumber(Dialogue dialogue, string dialogueActor)
    {
        ClearLinesAnswers(dialogue);
        currDialogueActor = dialogueActor;
        curResponseTracker = 0;
        nameText.text = dialogue.name;
        //nameText2.text = dialogue.name;
        lvl1stateCalculator(currDialogueActor,dialogue);
    }

    public void Answer(Dialogue dialogue, string dialogueActor, int incomingResponseIndex)
    { 
        curResponseTracker = incomingResponseIndex;
        var currentLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().GetCurrentLevel();
        if(currentLevel == "Level 1")
        {
            if (currDialogueActor == "OldWoman")
            {
                lvl1stateCalculator(currDialogueActor, dialogue);
                if (lvl1State == "babkaInitial")
                {
                    if (curResponseTracker == 0)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().helpedOldWoman = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().talkedToOldWoman = true;
                        GameObject.FindGameObjectWithTag("Bags").SetActive(false);

                    }
                    else if (curResponseTracker == 1)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().talkedToOldWoman = true;
                        EndDialogue();
                    }
                }
                if (lvl1State == "babkaWantsToGiveVodka")
                {
                    if (curResponseTracker == 0)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().hasVodka = true;
                        //add vodka to inventory
                        GetItemToInventory("VodkaButton");
                        EndDialogue();
                    }
                }
                if (lvl1State == "babkeDuet")
                {
                    if (curResponseTracker == 0)
                    {
                        EndDialogue();
                    }
                }
                if (lvl1State == "babkaSpit")
                {
                    if (curResponseTracker == 0)
                    {
                        EndDialogue();
                    }
                }

            }
            else if (currDialogueActor == "OldWomansBed")
            {
                lvl1stateCalculator(currDialogueActor, dialogue);
                if (lvl1State == "victory")
                {
                    playerData.carma = "good";
                    playerData.enterCutSceneShown = true;
                    playerData.currentLevelIndex = 3;
                    EndDialogue();
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().ShowOutro();
                }
                else if (lvl1State == "defeat")
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().ReloadLevel();
                }
                else if (lvl1State == "warn")
                {
                    EndDialogue();
                }
                else if (lvl1State == "helpReminder")
                {
                    EndDialogue();
                }
            }
            else if (currDialogueActor == "Plumber")
            {
                lvl1stateCalculator(currDialogueActor, dialogue);
                if (lvl1State == "bezVochilyNenado")
                {
                    if (curResponseTracker == 0)
                    {
                        EndDialogue();
                    }
                }
                else if(lvl1State == "giveVodka")
                {
                    if (curResponseTracker == 1)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().vodkaGivenToPlumber = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().hasVodka = false;
                        //TODO: remove Vodka from inventory
                        RemoveItemFromSlotWithoutDropping("VodkaItem");
                        GameObject.FindGameObjectWithTag("Plumber").GetComponent<PlumberScript>().changePlumberAnimationToHappy();
                        lvl1stateCalculator(currDialogueActor, dialogue);
                    }
                    else if (curResponseTracker == 0)
                    {
                        EndDialogue();
                    }
                }
                else if(lvl1State == "fix")
                {
                    if (curResponseTracker == 0)
                    {
                        //show option to leave[0]
                        EndDialogue();
                    }
                    else if (curResponseTracker == 1)
                    {
                        //option to ask to fix treadMill[1]
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().treadmillFixed = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().hasScrewKey = false;
                        //remove screwkey from inventory
                        RemoveItemFromSlotWithoutDropping("ScrewKeyItem");
                        GameObject.FindGameObjectWithTag("Plumber").GetComponent<PlumberScript>().plumberFixesTreadmill();
                        EndDialogue();
                    }
                    else if (curResponseTracker == 2)
                    {
                        //option to close window [2]
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().windowClosed = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().hasScrewKey = false;
                        //remove screwkey from inventory
                        RemoveItemFromSlotWithoutDropping("ScrewKeyItem");
                        EndDialogue();
                        GameObject.FindGameObjectWithTag("Plumber").GetComponent<PlumberScript>().plumberFixesWindow();
                        GameObject.FindGameObjectWithTag("OldWoman").GetComponent<OldWomanScript>().changeOldWomanAnimationToSleeping();
                        
                    }
                }
                else if (lvl1State == "noScrewKey")
                {
                    if (curResponseTracker == 0)
                    {
                        //set dialogue line plumber has no screw key
                        //TimeoutCorutine();
                        EndDialogue();
                    }
                }
                else if (lvl1State == "fixWindowOnly")
                {
                    if (curResponseTracker == 0)
                    {
                        TimeoutCorutine();
                        EndDialogue();                    
                    }
                    else if (curResponseTracker == 1)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().windowClosed = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().hasScrewKey = false;
                        //remove screwkey from inventory
                        RemoveItemFromSlotWithoutDropping("ScrewKeyItem");
                        EndDialogue();
                        GameObject.FindGameObjectWithTag("Plumber").GetComponent<PlumberScript>().plumberFixesWindow();
                        GameObject.FindGameObjectWithTag("OldWoman").GetComponent<OldWomanScript>().changeOldWomanAnimationToSleeping();
                    }
                }
                else if (lvl1State == "smthingFixed")
                {
                    if (curResponseTracker == 0)
                    {
                        EndDialogue();
                    }
                }
            }
            else if (currDialogueActor == "Hunk" || currDialogueActor == "Hunk2" || currDialogueActor == "Hunk3")
            {
                if(lvl1State == "hunksInitial")
                {
                    if(curResponseTracker == 0)
                    {
                        EndDialogue();
                    }
                }
                else if (lvl1State == "plumberGood")
                {
                    if(curResponseTracker == 0)
                    {
                        EndDialogue();
                    }
                    else if(curResponseTracker == 1)
                    {
                        treadmillFixed = true;
                    }
                }
                else if (lvl1State == "warn")
                {
                    if(curResponseTracker == 0)
                    {
                        EndDialogue();
                    }
                }
                else if (lvl1State == "defeat")
                {
                    if(curResponseTracker == 0)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().ReloadLevel();
                    }
                }
                else if (lvl1State == "victory")
                {
                    if(curResponseTracker == 0)
                    {
                        playerData.carma = "bad";
                        playerData.enterCutSceneShown = true;
                        playerData.currentLevelIndex = 3;
                        EndDialogue();
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().ShowOutro();
                    }
                }
            }
            else if (currDialogueActor == "SemenLvl1Intro")
            {
                EndDialogue();
            }
            else if (currDialogueActor == "SemenLvl1Outro")
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().LoadNextLevel(playerData);
            }
            lvl1stateCalculator(currDialogueActor, dialogue);
        }
        else if(currentLevel == "Level 2")
        {
            if (currDialogueActor == "cardsMan")
            {
                lvl2stateCalculator(currDialogueActor, dialogue);
                if (lvl2State == "initial")
                {
                    if (curResponseTracker == 0)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().talkedToCardsMan = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wonCardsMan = true;
                        GetItemToInventory("CoinsButton");
                    }
                    else if (curResponseTracker == 1)
                    {
                        EndDialogue();
                    }
                }
                else if(lvl2State == "won1stTime")
                {
                    if(curResponseTracker == 0)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().lostToCardsMan1stTime = true;
                        RemoveItemFromSlotWithoutDropping("CoinsItem");
                    }
                    else if (curResponseTracker == 1)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().rejectedToPlay2ndTime = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().hasMoney = true;
                    }
                }
                else if (lvl2State == "lost2ndTime")
                {
                    if(curResponseTracker == 0)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().rejectedToPlay3rdTime = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().lostToCardsMan2ndTime = true;
                    }
                    else if(curResponseTracker == 1)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().lostToCardsMan2ndTime = true;
                    }
                }
                else if (lvl2State == "gameOver")
                {
                    EndDialogue();
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().ReloadLevel();
                    
                }
                else if (lvl2State == "rejectedAfter2ndPlay")
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().rejectedToPlay3rdTime = false;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().lostToCardsMan2ndTime = false;
                    EndDialogue();
                }
                else if (lvl2State == "rejectedAfter1stPlay")
                {
                    EndDialogue();
                }
            }
            else if(currDialogueActor == "fatMan")
            {
                if(lvl2State == "initialNoMoney")
                {
                    if(curResponseTracker == 0)
                    {
                        EndDialogue();
                    }
                }
                else if(lvl2State == "initialHasMoney")
                {
                    if(curResponseTracker == 0)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().boughtPirogi = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().hasMoney = false;
                        RemoveItemFromSlotWithoutDropping("CoinsItem");
                        EndDialogue();
                    }
                    else if (curResponseTracker == 1)
                    {
                        EndDialogue();
                    }
                }
                else if (lvl2State == "cardsManCought")
                {
                    if(curResponseTracker == 0 
                        || curResponseTracker == 1
                        || curResponseTracker == 3)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().is1stRiddleAttempted = true;
                    }
                    else if (curResponseTracker == 2)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().is1stRiddleAttempted = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().is1stRiddleSolved = true;
                    }
                    else if (curResponseTracker == 4)
                    {
                        EndDialogue();
                    }
                }
                else if (lvl2State == "1stRiddleSolved")
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().sharedPirogyWithYou = true;
                    EndDialogue();
                }
                else if (lvl2State == "1stRiddleLost")
                {
                    if(curResponseTracker == 0 
                        || curResponseTracker == 1
                        || curResponseTracker == 3)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().is2ndRiddleAttempted = true;
                    }
                    else if (curResponseTracker == 2)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().is2ndRiddleSolved = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().is2ndRiddleAttempted = true;
                    }
                    else if (curResponseTracker == 4)
                    {
                        EndDialogue();
                    }

                }
                else if (lvl2State == "2ndRiddleSolved")
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().sharedPirogyWithYou = true;
                    EndDialogue();
                }
                else if(lvl2State == "hint")
                {
                    if(curResponseTracker == 0)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wasHintHelpful = true;
                    }
                    else if(curResponseTracker == 1)
                    {
                        //nothing happens here because fatMan gives player another try
                    }
                    else if(curResponseTracker == 2)
                    {
                        EndDialogue();
                    }
                }
                else if(lvl2State == "boughtPirogiOrSharedWithYoy")
                {
                    EndDialogue();
                }
            }
            else if(currDialogueActor == "milf")
            {
                if (lvl2State == "initial")
                {
                    if (curResponseTracker == 0 || curResponseTracker == 1)
                    {
                        //wrong option
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfAttempt1Done = true;
                    }
                    else if (curResponseTracker == 2)
                    {
                        EndDialogue();
                    }
                    else if (curResponseTracker == 3)
                    {
                        //right option
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfAttempt1Done = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfRightAnswer1 = true;

                    }
                }
                else if (lvl2State == "failedInitial")
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfAttempt1Done = false;
                    EndDialogue();
                }
                else if (lvl2State == "Step1")
                {
                    if (curResponseTracker == 0 || curResponseTracker == 1)
                    {
                        //wrong option
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfAttempt2Done = true;
                    }
                    else if (curResponseTracker == 2)
                    {
                        //right option
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfAttempt2Done = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfRightAnswer2 = true;

                    }
                    else if (curResponseTracker == 3)
                    {
                        EndDialogue();
                    }
                }
                else if (lvl2State == "Step1Failed")
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfAttempt2Done = false;
                    EndDialogue();
                }
                else if (lvl2State == "Step1Passed")
                {
                    if (curResponseTracker == 0 || curResponseTracker == 1)
                    {
                        //wrong option
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfAttempt3Done = true;
                    }
                    else if (curResponseTracker == 2)
                    {
                        //right option
                        IList milfBed = Resources.FindObjectsOfTypeAll<milfBedScript>();
                        foreach (milfBedScript i in milfBed)
                        {
                            i.ShowBed();
                        }
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfAttempt3Done = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfRightAnswer3 = true;
                    }
                    else if (curResponseTracker == 3)
                    {
                        EndDialogue();
                    }
                }
                else if (lvl2State == "Step2Failed")
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfAttempt3Done = false;
                    EndDialogue();
                }
                else if (lvl2State == "Step2Passed")
                {
                    //dono if there is something to set
                    EndDialogue();
                }
            }
            else if(currDialogueActor == "emergencyButton")
            {
                lvl2stateCalculator(currDialogueActor, dialogue);
                if (lvl2State == "initial")
                {
                    if (curResponseTracker == 0)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wireInTopSpot = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().isButtonPushed = true;
                        string newInfoText = "Провод в верхнем пазе";
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().ShowInfoText(newInfoText);
                    }
                    else if (curResponseTracker == 1)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wireInMiddleSpot = true;
                        string newInfoText = "Провод в среднем пазе";
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().ShowInfoText(newInfoText);
                    }
                    else if (curResponseTracker == 2)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wireInBottomSpot = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().isButtonPushed = true;
                        string newInfoText = "Провод в нижнем пазе";
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().ShowInfoText(newInfoText);
                    }
                    else if (curResponseTracker == 3)
                    {
                        if (GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wireInMiddleSpot)
                        {
                            //dirty hack:
                            GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().isButtonFixed = true;
                            // >> "fixedButton"
                        }
                        else
                        {
                            GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wireInBottomSpot = true;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().isButtonPushed = true;
                            // >> "nothingHappens"
                        }
                    }
                    else if (curResponseTracker == 4)
                    {
                        EndDialogue();
                    }
                    //wrong [0] >> change position
                    //right [1] >> change position
                    //wrong [2] >> change position
                    //push button [3] "nothingHappens" or "fixedButton"
                    //quit [0]
                }
                else if (lvl2State == "nothingHappens")
                {
                    
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().isButtonPushed = false;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wireInTopSpot = false;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wireInMiddleSpot = false;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wireInBottomSpot = false;
                    
                    //return back to "initial" [0]
                }
                else if (lvl2State == "onTheRightWay")
                {
                    if (curResponseTracker == 0)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wireInTopSpot = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().isButtonPushed = true;
                        string newInfoText = "Провод в верхнем пазе";
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().ShowInfoText(newInfoText);
                    }
                    else if (curResponseTracker == 1)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wireInMiddleSpot = true;
                        string newInfoText = "Провод в среднем пазе";
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().ShowInfoText(newInfoText);
                    }
                    else if (curResponseTracker == 2)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wireInBottomSpot = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().isButtonPushed = true;
                        string newInfoText = "Провод в нижнем пазе";
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().ShowInfoText(newInfoText);
                    }
                    else if (curResponseTracker == 3)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().isButtonFixed = true;
                    }
                    else if (curResponseTracker == 4)
                    {
                        EndDialogue();
                    }
                }
                else if (lvl2State == "fixedButton")
                {
                    if (curResponseTracker == 0)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().isButtonPushed = true;
                    }
                    else if (curResponseTracker == 1)
                    {
                        EndDialogue();
                    }
                    //click button [0] >> "call"
                    //quit [1]
                }
                else if (lvl2State == "call")
                {
                    if(curResponseTracker == 0)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wasCrimeReported = true;
                        IList cardsManBed = Resources.FindObjectsOfTypeAll<cardsManBedScript>();
                        foreach (cardsManBedScript i in cardsManBed)
                        {
                            i.ShowBed();
                        }
                    }
                    else if (curResponseTracker == 1)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wasCrimeReported = false;
                        EndDialogue();
                    }
                }
                else if (lvl2State == "answeredLeutenant")
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().allDone = true;
                    EndDialogue();
                }
                else if (lvl2State == "allDone")
                {
                    EndDialogue();
                }
            }
            else if(currDialogueActor == "cardsManBed")
            {
                if (lvl2State == "hungry")
                {
                    EndDialogue();
                }
                else if (lvl2State == "win")
                {
                    playerData.carma = "bad";
                    playerData.enterCutSceneShown = true;
                    playerData.currentLevelIndex = 4;
                    EndDialogue();
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().ShowOutro();
                    //GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().LoadNextLevel(playerData);
                }
            }
            else if(currDialogueActor == "milfBed")
            {
                if (lvl2State == "hungry")
                {
                    EndDialogue();
                }
                else if (lvl2State == "win")
                {
                    playerData.carma = "good";
                    playerData.enterCutSceneShown = true;
                    playerData.currentLevelIndex = 4;
                    EndDialogue();
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().ShowOutro();
                    //GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().LoadNextLevel(playerData);
                }
            }
            else if(currDialogueActor == "SemenLvl2Intro")
            {
                EndDialogue();
            }
            else if (currDialogueActor == "SemenLvl2Outro")
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().LoadNextLevel(playerData);
            }
            lvl2stateCalculator(currDialogueActor, dialogue);
        }
        curResponseTracker = 0;
    }
    void EndDialogue()
    {
        Debug.Log("End of conversation");
        GameObject.FindObjectOfType<DialogueBoxHandler>().GetComponent<DialogueBoxHandler>().CloseDialogueBox();
        GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().dialougeBoxOpen = false;
    }

    void lvl1stateCalculator (string currDialogueActor, Dialogue dialogue)
    {
        hasVodka = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().hasVodka;
        helpedOldWoman = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().helpedOldWoman;
        talkedToOldWoman = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().talkedToOldWoman;
        windowClosed = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().windowClosed;
        treadmillFixed = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().treadmillFixed;
        hasScrewKey = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().hasScrewKey;
        talkedToHunks = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().talkedToHunks;
        clickedOnWindow = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().clickedOnWindow;
        vodkaGivenToPlumber = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().vodkaGivenToPlumber;
        clicksOnOldWomanBed = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().clicksOnOldWomanBed;
        clickedOnHunks = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().clickedOnHunks;
        informedHunksTreadmillIsFixed = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().informedHunksTreadmillIsFixed;
        ClearLinesAnswers(dialogue);
        //calc the state on the 1st level
        string path = Application.dataPath;
        string pathToDialogue = path + "/StreamingAssets/DialogueStorage/"+currDialogueActor+".json";
        var jsonString = System.IO.File.ReadAllText(pathToDialogue);
        LinesAnswersList characterLinesAnswers =  JsonConvert.DeserializeObject<LinesAnswersList>(jsonString);
        if (currDialogueActor == "Plumber")
        {

            if (hasVodka == false && vodkaGivenToPlumber == false)
            {
                //plumber says he is not going to talk to you
                // option leave [0]
                lvl1State = "bezVochilyNenado";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
            }
            else if (hasVodka == true && vodkaGivenToPlumber == false)
            {
                lvl1State = "giveVodka";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
                // option to give vodka [0]
                // option leave [1]
            }
            else if (vodkaGivenToPlumber == true && hasVodka == false && windowClosed == false && treadmillFixed == false)
            {
                if ((talkedToHunks == true && hasScrewKey == true))
                {
                    lvl1State = "fix";
                    //show option to leave[0] and option to ask to fix treadMill[1] and option to close window [2]
                    SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
                }
                else if ((talkedToHunks == true && hasScrewKey == false) || (talkedToHunks == false && hasScrewKey == false))
                {
                     lvl1State = "noScrewKey";
                    // plumber says he could fix something but no instrument
                    //show option to leave [0]
                    SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
                }
                else if (talkedToHunks == false && hasScrewKey == true)
                {
                    lvl1State = "fixWindowOnly";
                    SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
                }
            }
            else if (windowClosed == true || treadmillFixed == true)
            {
                lvl1State = "smthingFixed";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
            }
        }
        else if (currDialogueActor == "OldWoman")
        {
            if(helpedOldWoman == false && windowClosed == false && hasVodka == false)
            {
                lvl1State = "babkaInitial";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
            }
            else if (helpedOldWoman == true && windowClosed == false && hasVodka == false && treadmillFixed == false)
            {
                lvl1State = "babkaWantsToGiveVodka";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
            }
            else if (helpedOldWoman == true && ((windowClosed == false && treadmillFixed == true) || (windowClosed == false && treadmillFixed == false)))
            {
                lvl1State = "babkeDuet";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
            }
            else if (helpedOldWoman == true && windowClosed == true)
            {
                lvl1State = "babkaSpit";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
            }
        }
        else if(currDialogueActor == "OldWomansBed")
        {
            if (windowClosed == true && helpedOldWoman == true) 
            {
                lvl1State = "victory";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
            }
            else if(windowClosed == false && helpedOldWoman == true)
            {
                clicksOnOldWomanBed = clicksOnOldWomanBed + 1;
                if(clicksOnOldWomanBed > 2)
                {
                    lvl1State = "defeat";
                    SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
                }
                else
                {
                    lvl1State = "warn";
                    SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
                }
            }
            else
            {
                lvl1State = "helpReminder";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
            }
        }
        else if (currDialogueActor == "Hunk" || currDialogueActor == "Hunk2" || currDialogueActor == "Hunk3")
        {
            if (talkedToHunks == false && windowClosed == false)
            {
                lvl1State = "hunksInitial";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
                GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().talkedToHunks = true;
            }
            else if (talkedToHunks == true && windowClosed == false && treadmillFixed == false)
            {
                lvl1State = "hunksInitial";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
            }
            else if (treadmillFixed == true && informedHunksTreadmillIsFixed == false)
            {
                lvl1State = "plumberGood";
                GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().informedHunksTreadmillIsFixed = true;
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
            }
            else if (treadmillFixed == false && clickedOnHunks < 2)
            {
                lvl1State = "warn";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
            }
            else if (treadmillFixed == false && clickedOnHunks >= 2)
            {
                lvl1State = "defeat";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
            }
            else if (treadmillFixed == true && informedHunksTreadmillIsFixed == true)
            {
                lvl1State = "victory";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl1State);
            }
        }
        else if (currDialogueActor == "SemenLvl1Intro")
        {
            lvl2State = "intro";
            SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
        }
        else if (currDialogueActor == "SemenLvl1Outro")
        {
            lvl2State = "outro";
            SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
        }
    }
    void lvl2stateCalculator (string currDialogueActor, Dialogue dialogue)
    {
        //----cardsMan---//
        bool talkedToCardsMan = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().talkedToCardsMan;
        bool wonCardsMan = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wonCardsMan;
        bool lostToCardsMan1stTime = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().lostToCardsMan1stTime;
        bool lostToCardsMan2ndTime = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().lostToCardsMan2ndTime;
        bool rejectedToPlay2ndTime = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().rejectedToPlay2ndTime;
        bool rejectedToPlay3rdTime = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().rejectedToPlay3rdTime;
        //----fatMan---//
        bool hasMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().hasMoney;
        bool boughtPirogi = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().boughtPirogi;
        bool is1stRiddleSolved = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().is1stRiddleSolved;
        bool is1stRiddleAttempted = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().is1stRiddleAttempted;
        bool is2ndRiddleAttempted = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().is2ndRiddleAttempted;
        bool is2ndRiddleSolved = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().is2ndRiddleSolved;
        bool wasHintHelpful = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wasHintHelpful;
        bool sharedPirogyWithYou = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().sharedPirogyWithYou;

        //----milf---//
        bool milfRightAnswer1 = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfRightAnswer1;
        bool milfRightAnswer2 = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfRightAnswer2;
        bool milfRightAnswer3 = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfRightAnswer3;
        bool milfAttempt1Done = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfAttempt1Done;
        bool milfAttempt2Done = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfAttempt2Done;
        bool milfAttempt3Done = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().milfAttempt3Done;

        //----emergencyButton---//
        bool isButtonFixed = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().isButtonFixed;
        bool isButtonPushed = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().isButtonPushed;
        bool wasCrimeReported = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wasCrimeReported;
        bool wireInTopSpot = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wireInTopSpot;
        bool wireInBottomSpot = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wireInBottomSpot;
        bool wireInMiddleSpot = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().wireInMiddleSpot;
        bool allDone = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().allDone;
        //----------------------//
        ClearLinesAnswers(dialogue);
        //calc the state on the 2nd level
        string path = Application.dataPath;
        string pathToDialogue = path + "/StreamingAssets/DialogueStorage/"+currDialogueActor+".json";
        var jsonString = System.IO.File.ReadAllText(pathToDialogue);
        LinesAnswersList characterLinesAnswers =  JsonConvert.DeserializeObject<LinesAnswersList>(jsonString);
        if (currDialogueActor == "cardsMan")
        {
            if (talkedToCardsMan == false)
            {
                lvl2State = "initial";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // play [0]
                // quit [1]
            }
            else if (talkedToCardsMan == true && wonCardsMan == true
                && lostToCardsMan1stTime == false && lostToCardsMan2ndTime == false
                && rejectedToPlay2ndTime == false && rejectedToPlay3rdTime == false)
            {
                lvl2State = "won1stTime";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // play 2nd time [0]
                // quit [1]
            }
            else if (talkedToCardsMan == true && wonCardsMan == true
                && lostToCardsMan1stTime == true && lostToCardsMan2ndTime == false
                && rejectedToPlay2ndTime == false && rejectedToPlay3rdTime == false)
            {
                lvl2State = "lost2ndTime";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // quit [0]
                // play 3rd time [1]
            }
            else if (talkedToCardsMan == true && wonCardsMan == true
                && lostToCardsMan1stTime == true && lostToCardsMan2ndTime == true
                && rejectedToPlay2ndTime == false && rejectedToPlay3rdTime == false)
            {
                lvl2State = "gameOver";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // game over [0]
            }
            else if (talkedToCardsMan == true && wonCardsMan == true
                && lostToCardsMan1stTime == false && lostToCardsMan2ndTime == false
                && rejectedToPlay2ndTime == true && rejectedToPlay3rdTime == false)
            {
                lvl2State = "rejectedAfter1stPlay";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // quite [0]
            }
            else if (talkedToCardsMan == true && wonCardsMan == true
                && lostToCardsMan1stTime == true && lostToCardsMan2ndTime == true
                && rejectedToPlay2ndTime == false && rejectedToPlay3rdTime == true)
            {
                lvl2State = "rejectedAfter2ndPlay";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // quite [0]
            }
        }
        else if (currDialogueActor == "fatMan")
        {
            if (!hasMoney && !boughtPirogi && !wasCrimeReported && !is1stRiddleSolved && !is1stRiddleAttempted
                && !is2ndRiddleSolved && !is2ndRiddleAttempted && !wasHintHelpful && !sharedPirogyWithYou)
            {
                lvl2State = "initialNoMoney";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // quit [0]
            }
            else if (hasMoney && !boughtPirogi && !wasCrimeReported && !is1stRiddleSolved && !is1stRiddleAttempted
                && !is2ndRiddleSolved && !is2ndRiddleAttempted && !wasHintHelpful && !sharedPirogyWithYou)
            {
                lvl2State = "initialHasMoney";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // buy [0]
                // quit [1]
            }
            else if ((!hasMoney && boughtPirogi && !wasCrimeReported && !is1stRiddleSolved && !is1stRiddleAttempted
                && !is2ndRiddleSolved && !is2ndRiddleAttempted && !wasHintHelpful && !sharedPirogyWithYou) || (sharedPirogyWithYou))
            {
                lvl2State = "boughtPirogiOrSharedWithYoy";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // quit [0]
            }
            else if (wasCrimeReported && !is1stRiddleSolved && !is1stRiddleAttempted
                && !is2ndRiddleSolved && !is2ndRiddleAttempted && !wasHintHelpful && !sharedPirogyWithYou)
            {
                lvl2State = "cardsManCought";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // wrong [0]
                // wrong [1]
                // right [2]
                // wrong [3]
                // quit [4]
            }
            else if (wasCrimeReported && is1stRiddleSolved && is1stRiddleAttempted
                && !is2ndRiddleSolved && !is2ndRiddleAttempted && !wasHintHelpful && !sharedPirogyWithYou)
            {
                lvl2State = "1stRiddleSolved";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // accept and quit [0]
            }
            else if (wasCrimeReported && !is1stRiddleSolved && is1stRiddleAttempted
                && !is2ndRiddleSolved && !is2ndRiddleAttempted && !wasHintHelpful && !sharedPirogyWithYou)
            {
                lvl2State = "1stRiddleLost";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // wrong [0]
                // wrong [1]
                // right [2]
                // wrong [3]
                // quit [4]
            }
            else if (wasHintHelpful || (is2ndRiddleSolved && is2ndRiddleAttempted))
            {
                //either solved from 1st attempt or after a hint
                lvl2State = "2ndRiddleSolved";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // accept and quit [0]
            }
            else if (wasCrimeReported && !is1stRiddleSolved && is1stRiddleAttempted
                && !is2ndRiddleSolved && is2ndRiddleAttempted && !wasHintHelpful && !sharedPirogyWithYou)
            {
                lvl2State = "hint";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // right [0]
                // wrong [1]
                // quit [2]
            }
        }
        else if (currDialogueActor == "milf")
        {
            if (!milfAttempt1Done && !milfRightAnswer1
                && !milfAttempt2Done && !milfRightAnswer2
                && !milfAttempt3Done && !milfRightAnswer3)
            {
                lvl2State = "initial";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // wrong [0]
                // wrong [1]
                // quit [2]
                // right [3]
            }
            else if (milfAttempt1Done && !milfRightAnswer1
                && !milfAttempt2Done && !milfRightAnswer2
                && !milfAttempt3Done && !milfRightAnswer3)
            {
                lvl2State = "failedInitial";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                //quit [0]
                milfAttempt1Done = false; //or do it in answer function
            }
            else if (milfAttempt1Done && milfRightAnswer1
                && !milfAttempt2Done && !milfRightAnswer2
                && !milfAttempt3Done && !milfRightAnswer3)
            {
                lvl2State = "Step1";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                //wrong [0]
                //wrong [1]
                //right [2]
                //quit [3]
            }
            else if (milfAttempt1Done && milfRightAnswer1
                && milfAttempt2Done && !milfRightAnswer2
                && !milfAttempt3Done && !milfRightAnswer3)
            {
                lvl2State = "Step1Failed";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                //quit [0]
                milfAttempt2Done = false; //or in the answer function
            }
            else if (milfAttempt1Done && milfRightAnswer1
                && milfAttempt2Done && milfRightAnswer2
                && !milfAttempt3Done && !milfRightAnswer3)
            {
                lvl2State = "Step1Passed";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                //wrong [0]
                //wrong [1]
                //right [3]
                //quit [4]
            }
            else if (milfAttempt1Done && milfRightAnswer1
                && milfAttempt2Done && milfRightAnswer2
                && milfAttempt3Done && !milfRightAnswer3)
            {
                lvl2State = "Step2Failed";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                //quit [0]
                milfAttempt3Done = false; // or in the asnwer function
            }
            else if (milfAttempt1Done && milfRightAnswer1
                && milfAttempt2Done && milfRightAnswer2
                && milfAttempt3Done && milfRightAnswer3)
            {
                lvl2State = "Step2Passed";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                //quit [0]
            }
        }
        else if (currDialogueActor == "emergencyButton")
        {
            if (!isButtonFixed && !isButtonPushed && !wasCrimeReported && !wireInTopSpot && !wireInBottomSpot)
            {
                lvl2State = "initial";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                //wrong [0]
                //right [1]
                //wrong [2]
                //push button [3]
                //quit [0]
            }
            else if (!isButtonFixed && isButtonPushed && !wasCrimeReported && (wireInTopSpot || wireInBottomSpot) && !wireInMiddleSpot)
            {
                lvl2State = "nothingHappens";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                //wrong [0]
                //right [1]
                //wrong [2]
                //push button [3]
                // quit [0]
            }
            else if (!isButtonFixed && !isButtonPushed && !wasCrimeReported && !wireInTopSpot && wireInBottomSpot && !wireInMiddleSpot)
            {
                lvl2State = "onTheRightWay";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
            }
            else if (isButtonFixed && !isButtonPushed && !wasCrimeReported && !wireInTopSpot && wireInMiddleSpot && !wireInBottomSpot)
            {
                lvl2State = "fixedButton";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                //push button [0]
                // quit [1]
            }
            else if (isButtonFixed && isButtonPushed && !wasCrimeReported && !wireInTopSpot && wireInMiddleSpot && !wireInBottomSpot)
            {
                lvl2State = "call";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                //report [0]
                // quit [1]
            }
            else if (isButtonFixed && isButtonPushed && wasCrimeReported && !wireInTopSpot && wireInMiddleSpot && !wireInBottomSpot && !allDone)
            {
                lvl2State = "answeredLeutenant";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                //report [0]
                // quit [1]
            }
            else if (allDone)
            {
                lvl2State = "allDone";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
                // quit [0]
            }
        }
        else if (currDialogueActor == "cardsManBed")
        {
            if ((wasCrimeReported && boughtPirogi) || (wasCrimeReported && sharedPirogyWithYou))
            {
                lvl2State = "win";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
            }
            else
            {
                lvl2State = "hungry";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
            }
        }
        else if (currDialogueActor == "milfBed")
        {
            if (milfRightAnswer3 && (boughtPirogi || sharedPirogyWithYou))
            {
                lvl2State = "win";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
            }
            else
            {
                lvl2State = "hungry";
                SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
            }

        }
        else if (currDialogueActor == "SemenLvl2Intro")
        {
            lvl2State = "intro";
            SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
        }
        else if (currDialogueActor == "SemenLvl2Outro")
        {
            lvl2State = "outro";
            SetNewLinesAnswers(dialogue, characterLinesAnswers, lvl2State);
        }
    }

    void SetNewLinesAnswers(Dialogue dialogue, LinesAnswersList linesAnswersList, string lvl1State)
    {
        LinesAnswer lineResponse = linesAnswersList.LinesAnswers.Find(l => l.name == lvl1State);
        //udpate character dialogue line
        dialogText.text = lineResponse.line;
        //dialogueText2.text = lineResponse.line;
        Transform Grid = FindObjectOfType<AnswerGridControl>().transform;
        //adding prefabs to grid
        foreach(Response i in lineResponse.responses)
        {
            LineAnswer responseLine = Resources.Load<LineAnswer>("AnswerButtonPrefab");
            LineAnswer hello = Instantiate<LineAnswer>(responseLine, new Vector3 (0,0,0), Quaternion.identity, Grid);
            hello.curResponseIndex = i.id;
            hello.responseText.text = i.response;
            //dialogue.playerResponses.Add(i.response);
        }
        foreach(Response i in lineResponse.responses)
        {
            dialogue.playerResponses.Add(i.response);
        }
        //response.text = dialogue.playerResponses[0];
    }
    IEnumerator TimeoutCorutine()
    {
        yield return new WaitForSeconds(5);
    }
    void ClearLinesAnswers(Dialogue dialogue)
    {
        dialogue.playerResponses.Clear();
        dialogue.sentences.Clear();
        //TODO: clear all prefabs
        var responsesGrid = GameObject.FindGameObjectWithTag("ResponseGrid");
        responsesGrid.GetComponent<AnswerGridControl>().removeAllAnswers();
    }
    public void RemoveItemFromSlotWithoutDropping(string itemTypeToRemove)
    {
        Object[] slotsWithItemToRemove = Resources.FindObjectsOfTypeAll(typeof(Spawn)); //GameObject.FindGameObjectWithTag("Vodka").transform.parent.gameObject;
        foreach (Spawn i in slotsWithItemToRemove)
        {
            if (i.itemType == itemTypeToRemove)
            {
                GameObject slotWithItemToRemove  = i.transform.parent.gameObject;
                slotWithItemToRemove.GetComponent<Slot>().DropItemWithoutSpawn();
                break;
            }
        }
    }
    public void GetItemToInventory(string itemObjectName)
    {
        var inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        GameObject itemButton = Resources.Load<GameObject>(itemObjectName);
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                //Add item to inventory
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);
                break;
            }
        }
    }
    
}
