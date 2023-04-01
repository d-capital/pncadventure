using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneContent : MonoBehaviour
{

    string objectName = "CutScene";
    public Text StartTitle;
    public Text GameName;
    public Text Text1;
    public Text Text3;
    public Text Text4;
    public Text TextS;
    // Start is called before the first frame update
    void Start()
    {
        StartTitle.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "StartTitle");
        GameName.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "GameName");
        Text1.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Text1");
        Text3.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Text3");
        Text4.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Text4");
        TextS.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "TextS");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
