using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class badEnding : MonoBehaviour
{
    string objectName = "BadEnding";
    public Text text1;
    public Text text2;
    public Text text3;

    // Start is called before the first frame update
    void Start()
    {
        text1.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "text1");
        text2.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "text2");
        text3.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "text2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
