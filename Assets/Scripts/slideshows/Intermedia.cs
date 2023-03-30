using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intermedia : MonoBehaviour
{

    string objectName = "Intermedia";

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "text");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
