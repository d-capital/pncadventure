using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class Slot : MonoBehaviour
{
    public Inventory inventory;
    public int i;
    public string hint = "text";

    public void DropItem()
    {
        foreach(Spawn child in transform.GetComponentsInChildren<Spawn>())
        {
            child.GetComponent<Spawn>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
            transform.GetComponentInChildren<TMP_Text>().text = "";
        }
    }
    public void DropItemWithoutSpawn()
    {
        foreach (Spawn child in transform.GetComponentsInChildren<Spawn>())
        {
            GameObject.Destroy(child.gameObject);
            foreach (TMP_Text tmpText in transform.GetComponentsInChildren<TMP_Text>()) 
            {
                tmpText.text = "";
            };
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (transform.GetComponentsInChildren<Spawn>().Length <= 0)
        {
            inventory.isFull[i] = false;
        }
    }
}
