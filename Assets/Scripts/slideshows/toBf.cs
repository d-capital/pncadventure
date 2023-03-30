using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toBf : MonoBehaviour
{
    string objectName = "toBf";

    public Text text1;
    public Text text2;

    // Start is called before the first frame update
    void Start()
    {
        text1.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "text1");
        text2.text = GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "text2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
