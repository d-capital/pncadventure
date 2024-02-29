using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System;

public class LanguageManager : MonoBehaviour
{

    public string language = "en";

    public string jsonString;

    public bool isWebRequestDone;

    [System.Serializable]
    public class StaticTerm
    {
        public string name { get; set; }
        public string text { get; set; }
    }

    private void Start()
    {
        ResolveLanguage();
    }

    public void ResolveLanguage()
    {
        if (SaveSystem.LoadPlayer() != null)
        {
            if (SaveSystem.LoadPlayer().language != null)
            {
                language = SaveSystem.LoadPlayer().language;
            }
        }
    }

    public int getCorrectDropDownValue()
    {
        int languageValue = 0;
        if (language == "en") 
        {
            languageValue = 1;
        }
        return languageValue;
    }

    //------------------------------------------FOR TERM---------------------------------------------------------------------------------//
    public void getCorrectTerm(string objectName, string textName, Text textToSet, TMP_Text tmpTextToSet, string stringTextToSet)
    {
        ResolveLanguage();
        string fileName = objectName + ".json";
        string pathToTerm = Path.Combine(Application.streamingAssetsPath, language, fileName);
        GetTextFromCorrectPlaceForTerm(pathToTerm, textToSet, tmpTextToSet, stringTextToSet, textName);
    }
    public void GetTextFromCorrectPlaceForTerm(string path, Text textToSet, TMP_Text tmpTextToSet, string stringTextToSet, string key)
    {
        Debug.Log(path);
        if (path.Contains("://") || path.Contains(":///"))
        {
            Debug.Log("in the Url Resolver");
            StartCoroutine(GetAssetsFromUrlForTerm(path, textToSet, tmpTextToSet, stringTextToSet, key));
        }
        else
        {
            jsonString = System.IO.File.ReadAllText(path);
            string correctTerm = formTerm(jsonString, key);
            if (textToSet == null && tmpTextToSet == null)
            {
                stringTextToSet = correctTerm;
                GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().ShowInfoText(stringTextToSet);
            }
            else if (textToSet == null && stringTextToSet == "")
            {
                tmpTextToSet.text = correctTerm;
            }
            else
            {
                textToSet.text = correctTerm;
            }
        }
    }
    IEnumerator GetAssetsFromUrlForTerm(string pathToTerm, Text textToSet, TMP_Text tmpTextToSet, string stringTextToSet, string key)
    {
        Debug.Log("in the Unity Web Function");
        using (UnityWebRequest www = UnityWebRequest.Get(pathToTerm))
        {
            isWebRequestDone = false;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("something went wrong with connection");
            }
            else
            {
                jsonString = ASCIIEncoding.UTF8.GetString(www.downloadHandler.data);
                isWebRequestDone = true;
                Debug.Log("here is the string " + jsonString);
                string correctTerm = formTerm(jsonString, key);
                if (textToSet == null && tmpTextToSet == null)
                {
                    stringTextToSet = correctTerm;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AdvScript>().ShowInfoText(stringTextToSet);
                }
                else if (textToSet == null && stringTextToSet == "")
                {
                    tmpTextToSet.text = correctTerm;
                }
                else
                {
                    textToSet.text = correctTerm;
                }
                yield return jsonString;
            }
        }

    }
    public string formTerm(string jsonString, string key)
    {
        List<StaticTerm> staticTerms = JsonConvert.DeserializeObject<List<StaticTerm>>(jsonString);
        StaticTerm staticTerm = staticTerms.Find(s => s.name == key);
        string correctTerm = staticTerm.text;
        return correctTerm;
    }


    //------------------------------------------FOR NAME---------------------------------------------------------------------------------//
    public void getCorrectName(string characterTerm, Text textToSet, TMP_Text tmpTextToSet, Dialogue stringTextToSet)
    {
        ResolveLanguage();
        //string path = Application.dataPath;
        string fileName = "CharacterNames" + ".json";
        string pathToName = Path.Combine(Application.streamingAssetsPath, language, fileName);
        GetTextFromCorrectPlaceForName(pathToName, textToSet, tmpTextToSet, stringTextToSet, characterTerm);
    }
    public void GetTextFromCorrectPlaceForName(string path, Text textToSet, TMP_Text tmpTextToSet, Dialogue stringTextToSet, string key)
    {
        Debug.Log(path);
        if (path.Contains("://") || path.Contains(":///"))
        {
            Debug.Log("in the Url Resolver");
            StartCoroutine(GetAssetsFromUrlForName(path, textToSet, tmpTextToSet, stringTextToSet, key));
        }
        else
        {
            jsonString = System.IO.File.ReadAllText(path);
            string correctName = formName(jsonString, key);
            if (textToSet == null && tmpTextToSet == null)
            {
                stringTextToSet.name = correctName;
            }
            else if (textToSet == null && stringTextToSet == null)
            {
                tmpTextToSet.text = correctName;
            }
            else
            {
                textToSet.text = correctName;
            }
        }
    }
    IEnumerator GetAssetsFromUrlForName(string pathToTerm, Text textToSet, TMP_Text tmpTextToSet, Dialogue stringTextToSet, string key)
    {
        Debug.Log("in the Unity Web Function");
        using (UnityWebRequest www = UnityWebRequest.Get(pathToTerm))
        {
            isWebRequestDone = false;
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("something went wrong with connection");
            }
            else
            {
                jsonString = ASCIIEncoding.UTF8.GetString(www.downloadHandler.data);
                isWebRequestDone = true;
                Debug.Log("here is the string " + jsonString);
                string correctName = formName(jsonString, key);
                if (textToSet == null && tmpTextToSet == null)
                {
                    stringTextToSet.name = correctName;
                }
                else if (textToSet == null && stringTextToSet == null)
                {
                    tmpTextToSet.text = correctName;
                }
                else
                {
                    textToSet.text = correctName;
                }
                yield return jsonString;
            }
        }

    }

    public string formName(string jsonString, string key)
    {
        List<StaticTerm> characterNames = JsonConvert.DeserializeObject<List<StaticTerm>>(jsonString);
        StaticTerm displayedCharacterName = characterNames.Find(s => s.name == key);
        string correctName = displayedCharacterName.text;
        return correctName;
    }
    //-----------------------------------------------------------------------------------------------------------------------------------//
}
