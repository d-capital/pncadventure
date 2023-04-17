using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutScene0Content : MonoBehaviour
{
    string objectName = "CutScene0";
    public TMP_Text Text1;
    public TMP_Text Text2;
    public TMP_Text Text3;
    // Start is called before the first frame update
    void Start()
    {
        Text1.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Text1");
        Text2.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Text2");
        Text3.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Text3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
