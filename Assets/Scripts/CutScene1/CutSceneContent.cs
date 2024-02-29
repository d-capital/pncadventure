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

    public float Text1Time;
    public float Text3Time;
    public float Text4Time;
    public float TextSTime;

    public bool Text1Played;
    public bool Text3Played;
    public bool Text4Played;
    public bool TextSPlayed;

    string language;

    // Start is called before the first frame update
    void Start()
    {
        language = GameObject.FindObjectOfType<LanguageManager>().language;
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "StartTitle", StartTitle, null, "");
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "GameName", GameName, null, "");
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Text1", Text1, null, "");
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Text3", Text3, null, "");
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "Text4", Text4, null, "");
        GameObject.FindObjectOfType<LanguageManager>().getCorrectTerm(objectName, "TextS", TextS, null, "");
    }

    // Update is called once per frame
    void Update()
    {
        Text1Time -= Time.deltaTime;
        Text3Time -= Time.deltaTime;
        Text4Time -= Time.deltaTime;
        TextSTime -= Time.deltaTime;
        if (Text1Time <= 0 && !Text1Played)
        {
            Text1Played = true;
            GameObject.Find("voiceText1-" + language).GetComponent<AudioSource>().Play();

        }
        if (Text3Time <= 0 && !Text3Played)
        {
            Text3Played = true;
            GameObject.Find("voiceText3-" + language).GetComponent<AudioSource>().Play();
        }
        if (Text4Time <= 0 && !Text4Played)
        {
            Text4Played = true;
            GameObject.Find("voiceText4-" + language).GetComponent<AudioSource>().Play();
        }
        if (TextSTime <= 0 && !TextSPlayed)
        {
            TextSPlayed = true;
            GameObject.Find("voiceTextS-" + language).GetComponent<AudioSource>().Play();
        }
    }
}
