using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class LanguageManager : MonoBehaviour
{

    public string language = "ru";

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

    public string getCorrectTerm(string objectName, string textName)
    {
        string path = Application.dataPath;
        string pathToFile = path + "/StreamingAssets/" + language + "/" + objectName + ".json";
        var jsonString = System.IO.File.ReadAllText(pathToFile);
        List<StaticTerm> staticTerms = JsonConvert.DeserializeObject<List<StaticTerm>>(jsonString);
        StaticTerm staticTerm = staticTerms.Find(s => s.name == textName);
        string correctTerm = staticTerm.text;
        return correctTerm;
    }

    public string getCorrectName(string characterTerm)
    {
        string path = Application.dataPath;
        string pathToFile = path + "/StreamingAssets/" + language + "/" + "CharacterNames" + ".json";
        var jsonString = System.IO.File.ReadAllText(pathToFile);
        List<StaticTerm> characterNames = JsonConvert.DeserializeObject<List<StaticTerm>>(jsonString);
        StaticTerm displayedCharacterName = characterNames.Find(s => s.name == characterTerm);
        string correctName = displayedCharacterName.text;
        return correctName;
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

}
