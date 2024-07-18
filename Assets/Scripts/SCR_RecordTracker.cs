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

    [Header("Variable")]
    public string text;
    public KeyCode deleteRecords;

    void Update(){
        if (Input.GetKeyDown(deleteRecords)){
            PlayerPrefs.DeleteAll();
        }
    }

    public void NewLevel(string levelName){
        int levelRecord = PlayerPrefs.GetInt(levelName,int.MaxValue);
        if (levelRecord == int.MaxValue){
            recordIndicator.text = text + System.Environment.NewLine + "???";
        }else{
            recordIndicator.text = text + System.Environment.NewLine + levelRecord;
        }
    }

    public bool SetRecord(int attempt){ //returns whether a new record has been set
        int oldRecord = PlayerPrefs.GetInt(SCR_LevelGenerator.instance.levelName,int.MaxValue);
        if (attempt < oldRecord){
            PlayerPrefs.SetInt(SCR_LevelGenerator.instance.levelName , attempt);
            return true;
        }else{
            return false;
        }
    }
}
