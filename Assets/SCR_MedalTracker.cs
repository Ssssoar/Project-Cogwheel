using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_MedalTracker : MonoBehaviour{
    public SCR_Medallion medal;
    public SCR_SpawnLevelList levelSpawner;
    public Transform victoryMenu;
    string level;

    void NewMedal(string levelName){
        PlayerPrefs.SetInt(levelName,1);
        levelSpawner.pendingMedal = Instantiate<SCR_Medallion>(medal , new Vector3(Screen.height/2,Screen.width/2,0f) , Quaternion.identity, victoryMenu);
        levelSpawner.pendingMedalName = levelName;
    }

    public void CheckRecord(){
        if (SCR_RecordTracker.instance.CheckRecord(SCR_ClickCounter.instance.count)){ //if there is a new record set!
            NewMedal(SCR_RecordTracker.instance.currentLevel);
        }
    }
}
