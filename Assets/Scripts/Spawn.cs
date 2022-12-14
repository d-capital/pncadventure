using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spawn : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject item;
    public string itemType;
    private Transform player;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Vector3 initObjectPos;
    

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
            i.layer = 3;
        }
        returnObjectIfNeeded();
    }

    public void returnObjectIfNeeded()
    {
        StartCoroutine(checkIfObjectWasAppliedToTargetWithDelay());
    }

    IEnumerator checkIfObjectWasAppliedToTargetWithDelay()
    {
        yield return new WaitForSeconds(1.0f);
        bool toiletDoorReceivedCrowbar = GameObject.FindObjectOfType<toiletDoor>().GetComponent<toiletDoor>().objectReceived;
        bool sinkReceivedBathItems = GameObject.FindObjectOfType<sink>().GetComponent<sink>().objectReceived;
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
        Vector3 playerPos = new Vector3 (playerObj.transform.position.x + 3, playerObj.transform.position.y);
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
}
