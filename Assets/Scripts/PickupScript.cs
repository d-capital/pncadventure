using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupScript : MonoBehaviour
{
    
    private Inventory inventory;
    public GameObject itemButton;
    public Texture2D cursor;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void onClick()
    {

        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                //Add item to inventory
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);
                string hint = itemButton.GetComponentInChildren<Spawn>().itemType;
                inventory.slots[i].transform.GetComponentInChildren<TMP_Text>().text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("inventoryHints", hint);
                Destroy(gameObject);
                break;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (!FindObjectOfType<AdvScript>().dialougeBoxOpen)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseExit()
    {
        if (!FindObjectOfType<AdvScript>().dialougeBoxOpen)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
