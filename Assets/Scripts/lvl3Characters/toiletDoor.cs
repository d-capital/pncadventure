using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class toiletDoor : MonoBehaviour, IDropHandler
{

    public bool objectReceived = false;
    public AudioSource audioSource;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<Spawn>().item.name == "crowBar")
            {
                objectReceived = true;
                OpenDoor();
                GameObject.Destroy(eventData.pointerDrag);
                var DroppableItems = GameObject.FindGameObjectsWithTag("droppable");
                foreach (var i in DroppableItems)
                {
                    i.layer = 0;
                }
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

    public void OpenDoor()
    {
        gameObject.SetActive(false);
        audioSource.Play();
    }
}
