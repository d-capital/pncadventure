using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class toiletDoor : MonoBehaviour, IDropHandler
{


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<Spawn>().item.name == "crowBar")
            {
                OpenDoor();
                GameObject.Destroy(eventData.pointerDrag);
                //show message
                //set bool variable
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
    }
}
