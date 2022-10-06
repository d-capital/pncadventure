using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    public string itemType;
    private Transform player;
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
