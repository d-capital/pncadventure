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

    public float Text1Time;
    public float Text2Time;
    public float Text3Time;


    public bool Text1Played;
    public bool Text2Played;
    public bool Text3Played;

    string language;

    public string jsonString;
    // Start is called before the first frame update
    void Start()
    {
        language = GameObject.FindObjectOfType<LanguageManager>().language;
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Text1", null, Text1, "");
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Text2", null, Text2, "");
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Text3", null, Text3, "");
    }

    // Update is called once per frame
    void Update()
    {
        Text1Time -= Time.deltaTime;
        Text2Time -= Time.deltaTime;
        Text3Time -= Time.deltaTime;
        if (Text1Time <= 0 && !Text1Played)
        {
            Text1Played = true;
            GameObject.Find("voiceText1-" + language).GetComponent<AudioSource>().Play();

        }
        if (Text2Time <= 0 && !Text2Played)
        {
            Text2Played = true;
            GameObject.Find("voiceText2-" + language).GetComponent<AudioSource>().Play();
        }
        if (Text3Time <= 0 && !Text3Played)
        {
            Text3Played = true;
            GameObject.Find("voiceText3-" + language).GetComponent<AudioSource>().Play();
        }
    }
}
