using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LineAnswer : MonoBehaviour
{
    public GameObject LineAnswerPrefab;
    public string curResponseIndex;
    public Text responseText;
    //private Transform player;
    public void CreateNewResponse()
    {
        Transform Grid = FindObjectOfType<AnswerGridControl>().transform;
        Instantiate(LineAnswerPrefab, new Vector3 (0,0,0), Quaternion.identity, Grid);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Answer()
    {
        var incomingResponseIndex = Convert.ToInt32(curResponseIndex);
        var playerObj = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>();
        try
        {
            playerObj.Answer(incomingResponseIndex);
        }
        catch(NullReferenceException)
        {
            Debug.Log("Dialogue ended");
        }
        //playerObj.Answer(incomingResponseIndex);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
