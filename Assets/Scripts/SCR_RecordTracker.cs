using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_RecordTracker : MonoBehaviour{
    public static SCR_RecordTracker instance;
    private void Awake(){
        if (SCR_RecordTracker.instance != null) Destroy(SCR_RecordTracker.instance.gameObject);
        instance = this;
    }

    [Header("References")]
    public TMP_Text recordIndicator;
    public SCR_MedalTracker medalScript;

    [Header("Variable")]
    public string text;
    public string enText;

    int currentStar;
    public string currentLevel;

    public void NewLevel(string levelName , int starScore){
        string printableText = ((SCR_Language.instance != null) && (SCR_Language.instance.GetLang() == Lang.english)) ? enText : text;
        recordIndicator.text = printableText + System.Environment.NewLine + starScore;
        currentStar = starScore;
        currentLevel = levelName;
    }

    public bool CheckRecord(int attempt){ //returns whether a new medal was obtained
        PlayerPrefs.SetInt(currentLevel + "passed",1);
        if (attempt <= currentStar){
            return true;
        }else{
            return false;
        }
    }
}
