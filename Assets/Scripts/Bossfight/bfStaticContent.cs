using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bfStaticContent : MonoBehaviour
{
    string objectName = "bfStaticContent";

    public TMP_Text instructionText;
    public TMP_Text qteText;
    public TMP_Text bfInfoText;
    // Start is called before the first frame update
    void Start()
    {
        instructionText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "instructionText");
        qteText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "qteText");
        bfInfoText.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "bfInfoText");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
