using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_MedalTracker : MonoBehaviour{
    public void NewMedal(string levelName){
        PlayerPrefs.SetInt(levelName,1);
    }
}
