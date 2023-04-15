using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public bool isOpened = false;
    public bool canOpenInventory = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCloseInventory()
    {
        if (canOpenInventory)
        {
            var slots = Resources.FindObjectsOfTypeAll(typeof(Slot));
            var crosses = Resources.FindObjectsOfTypeAll(typeof(Cross));
            if (isOpened == false)
            {
                foreach (Slot i in slots)
                {
                    i.gameObject.SetActive(true);
                    if(i.GetComponentsInChildren<Spawn>().Length > 0)
                    {
                        string hint = i.GetComponentInChildren<Spawn>().itemType;
                        i.gameObject.GetComponentInChildren<TMP_Text>().text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm("inventoryHints", hint);
                    }
                    else
                    {
                        i.gameObject.GetComponentInChildren<TMP_Text>().text = "";
                    }
                }

                foreach (Cross i in crosses)
                {
                    i.gameObject.SetActive(true);
                }
                isOpened = true;
            }
            else
            {
                foreach (Slot i in slots)
                {
                    i.gameObject.SetActive(false);
                }

                foreach (Cross i in crosses)
                {
                    i.gameObject.SetActive(false);
                }
                isOpened = false;
            }
        }

    }
}
