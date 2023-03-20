using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spawn : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public GameObject item;
    public string itemType;
    private Transform player;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Vector3 initObjectPos;
    public bool objectReceived = false;
    public Texture2D cursor;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        var DroppableItems = GameObject.FindGameObjectsWithTag("droppable");
        foreach (var i in DroppableItems)
        {
            i.layer = 3;
        }
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        initObjectPos = rectTransform.position;
        gameObject.GetComponentInParent<Slot>().gameObject.GetComponent<Canvas>().overrideSorting = true;
        gameObject.GetComponentInParent<Slot>().gameObject.GetComponent<Canvas>().sortingOrder = 61;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //all exceptions shold go here
        //TODO: throws exception cause toilet door is not there yet on lvl1
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        var DroppableItems = GameObject.FindGameObjectsWithTag("droppable");
        foreach (var i in DroppableItems)
        {
            i.layer = 0;
        }
        returnObjectIfNeeded();
        gameObject.GetComponentInParent<Slot>().gameObject.GetComponent<Canvas>().overrideSorting = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void returnObjectIfNeeded()
    {
        StartCoroutine(checkIfObjectWasAppliedToTargetWithDelay());
    }

    IEnumerator checkIfObjectWasAppliedToTargetWithDelay()
    {
        yield return new WaitForSeconds(1.0f);
        toiletDoor toiletDoor = GameObject.FindObjectOfType<toiletDoor>();
        sink sink = GameObject.FindObjectOfType<sink>();
        bool toiletDoorReceivedCrowbar;
        bool sinkReceivedBathItems;
        if (toiletDoor != null)
        {
            toiletDoorReceivedCrowbar = GameObject.FindObjectOfType<toiletDoor>().GetComponent<toiletDoor>().objectReceived;
        }
        else
        {
            toiletDoorReceivedCrowbar = false;
        }
        if (sink != null)
        {
            sinkReceivedBathItems = GameObject.FindObjectOfType<sink>().GetComponent<sink>().objectReceived;
        }
        else
        {
            sinkReceivedBathItems = false;
        }
        if (!toiletDoorReceivedCrowbar || !sinkReceivedBathItems)
        {
            rectTransform.anchoredPosition = initObjectPos;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / GameObject.FindObjectOfType<Canvas>().GetComponent<Canvas>().scaleFactor;
    }
    public void SpawnDroppedItem()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().player;
        Vector3 playerPos = new Vector3(playerObj.transform.position.x + 3, playerObj.transform.position.y);
        Debug.Log(playerPos);
        Instantiate(item, playerPos, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            if (itemType == "glassOfTeaItem" && eventData.pointerDrag.GetComponent<Spawn>().item.name.Contains("boyaryshnik"))
            {
                int slotNumber = gameObject.GetComponentInParent<Slot>().GetComponent<Slot>().i;
                objectReceived = true;
                GameObject.Destroy(eventData.pointerDrag);
                GameObject.FindObjectOfType<DialogueManager>().GetComponent<DialogueManager>().RemoveItemFromSlotWithoutDropping("glassOfTeaItem");
                GameObject.FindObjectOfType<DialogueManager>().GetComponent<DialogueManager>().CraftItemInSlot("glassOfBoyaryshnikButton", slotNumber);
                GameObject.Find("boyaryshnikSound").GetComponent<AudioSource>().Play();
                var DroppableItems = GameObject.FindGameObjectsWithTag("droppable");
                foreach (var i in DroppableItems)
                {
                    i.layer = 0;
                }
                eventData.pointerDrag.GetComponentInParent<Slot>().gameObject.GetComponent<Canvas>().overrideSorting = false;
            }
            else
            {
                eventData.pointerDrag.gameObject.transform.position = eventData.pointerDrag.gameObject.GetComponent<Spawn>().initObjectPos;
            }
        }
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
