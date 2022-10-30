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

    IList dialogBoxesSearchResults;

    DialogueTrigger currentDialogueTrigger;
    DialogueTrigger oldWomanDialogue;
    DialogueTrigger oldWomanBedDialogue;
    DialogueTrigger plumberDialogue;
    DialogueTrigger hunkDialogue;
    DialogueTrigger hunk1Dialogue;
    DialogueTrigger hunk2Dialogue;
    DialogueBoxHandler dialogueBox;

    public string currentDialoguePartner;

    public Animator animator;
    public AudioSource collectSound;

    //public TextMeshProUGUI newInfoText;

    // Use this for initialization if something needs to be defined before the game starts
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
            }
            else
            {
                print("No collider hit!");
            }
        }
    }
    /*public void NextAnswer()
    {
        MapDialogueTrigger(currentDialoguePartner).NextAnswer();
    }
    public void PreviosAnswer()
    {
        MapDialogueTrigger(currentDialoguePartner).PreviosAnswer();
    }*/
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
        currentDialogueTrigger = GameObject.FindGameObjectWithTag(currentDialoguePartner).GetComponent<DialogueTrigger>();
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

}