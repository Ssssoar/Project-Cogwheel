using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_VersionWiper : MonoBehaviour{
    public float currentVersion;

    void Start(){
        if (PlayerPrefs.GetFloat("version") != currentVersion){
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetFloat("version",currentVersion);
        }
    }

    // Update is called once per frame
    void Update(){
        
    }
}
