using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class toiletDoor : MonoBehaviour, IDropHandler
{

    public bool objectReceived = false;
    public AudioSource audioSource;
    public Texture2D cursor;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<Spawn>().item.name == "crowBar")
            {
                objectReceived = true;
                OpenDoor();
                eventData.pointerDrag.GetComponent<Spawn>().GetComponentInParent<Slot>().GetComponentInChildren<TMP_Text>().text = "";
                GameObject.Destroy(eventData.pointerDrag);
                var DroppableItems = GameObject.FindGameObjectsWithTag("droppable");
                foreach (var i in DroppableItems)
                {
                    i.layer = 0;
                }
                eventData.pointerDrag.GetComponentInParent<Slot>().gameObject.GetComponent<Canvas>().overrideSorting = false;
                //show message
                //set bool variable
            }
            else
            {
                eventData.pointerDrag.gameObject.transform.position = eventData.pointerDrag.gameObject.GetComponent<Spawn>().initObjectPos;
                eventData.pointerDrag.GetComponentInParent<Slot>().gameObject.GetComponent<Canvas>().overrideSorting = false;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        gameObject.SetActive(false);
        audioSource.Play();
    }

    private void OnMouseOver()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
