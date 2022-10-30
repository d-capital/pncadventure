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
        lvl1stateCalculator(currDialogueActor,dialogue);
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

    /*public void PreviosAnswer (Dialogue dialogue)
    {
        Debug.Log("Current Length of the responses array " + dialogue.playerResponses.Count);
        Debug.Log("Current Response Index " + curResponseTracker);
        futResponseTracker = curResponseTracker - 1;
        if(curResponseTracker > 0 && futResponseTracker <= dialogue.playerResponses.Count - 1)
        {
            curResponseTracker = futResponseTracker;
            Debug.Log("Future Response Index " + curResponseTracker);
            FindObjectOfType<DialogueManager>().response.text = dialogue.playerResponses[curResponseTracker];
        }

    }
    public void NextAnswer (Dialogue dialogue)
    {
        
        Debug.Log("Current Length of the responses array " + dialogue.playerResponses.Count);
        Debug.Log("Current Response Index " + curResponseTracker);
        futResponseTracker = curResponseTracker + 1;
        if(curResponseTracker >= 0 && futResponseTracker <= dialogue.playerResponses.Count - 1)
        {
            curResponseTracker = futResponseTracker;
            Debug.Log("Future Response Index " + curResponseTracker);
            FindObjectOfType<DialogueManager>().response.text = dialogue.playerResponses[curResponseTracker];
        }

    }*/

    public void Answer(Dialogue dialogue, string dialogueActor, int incomingResponseIndex)
    { 
        //TODO: Get curResponseTracker from the index of the prefab
        curResponseTracker = incomingResponseIndex;
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
                    var inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
                    GameObject vodkaButton = Resources.Load<GameObject>("VodkaButton");

                    for (int i = 0; i < inventory.slots.Length; i++)
                        {
                            if (inventory.isFull[i] == false)
                            {
                                //Add item to inventory
                                inventory.isFull[i] = true;
                                Instantiate(vodkaButton, inventory.slots[i].transform, false);
                                //Destroy(gameObject);
                                break;
                            }
                        }
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
                PlayerData playerData = new PlayerData();
                playerData.carma = "good";
                playerData.enterCutSceneShown = true;
                playerData.currentLevelIndex = 3;
                GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().LoadNextLevel(playerData);
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
                    PlayerData playerData = new PlayerData();
                    playerData.carma = "bad";
                    playerData.enterCutSceneShown = true;
                    playerData.currentLevelIndex = 3;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().LoadNextLevel(playerData);
                }
            }
        }
        lvl1stateCalculator(currDialogueActor, dialogue);
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
}
