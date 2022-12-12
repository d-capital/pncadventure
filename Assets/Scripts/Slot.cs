using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int i;

    public void DropItem()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
        }
    }
    public void DropItemWithoutSpawn()
    {
        foreach(Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
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
        if (transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }
    }
}
