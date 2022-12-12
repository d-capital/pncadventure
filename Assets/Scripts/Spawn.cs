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
    private Vector3 initObjectPos;
    

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
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        initObjectPos = rectTransform.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //all exceptions shold go here
        //TODO: throws exception cause toilet door is not there yet on lvl1
        try
        {
            Vector3 toiletDoor = GameObject.FindObjectOfType<toiletDoor>().transform.position;
            Vector3 sink = GameObject.FindObjectOfType<sink>().transform.position;
            if (rectTransform.position != toiletDoor && rectTransform.position != sink)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = initObjectPos;
            }
        }
        catch
        {
            Debug.Log("no target objects");
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = initObjectPos;
            Debug.Log("OnEndDrag");
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
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
