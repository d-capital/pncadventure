using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class sink : MonoBehaviour, IDropHandler
{

    public bool objectReceived = false;
    public Texture2D cursor;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<Spawn>().item.name == "bathItems")
            {
                objectReceived = true;
                GameObject.Destroy(eventData.pointerDrag);
                AdvScript PlayerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>();
                PlayerObject.bathed = true;
                string InfoText = "Умытым быть гораздо лучше";
                PlayerObject.ShowInfoText(InfoText);
                var DroppableItems = GameObject.FindGameObjectsWithTag("droppable");
                foreach (var i in DroppableItems)
                {
                    i.layer = 0;
                }
                GameObject.FindObjectOfType<DialogueManager>().completeTask(0);
                //show message
                //set bool variable
            }
            else
            {
                eventData.pointerDrag.gameObject.transform.position = eventData.pointerDrag.gameObject.GetComponent<Spawn>().initObjectPos;
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

    private void OnMouseOver()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
