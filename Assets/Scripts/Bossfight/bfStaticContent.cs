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
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "instructionText", null, instructionText, "");
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "qteText", null, qteText, "");
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "bfInfoText", null, bfInfoText, "");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
