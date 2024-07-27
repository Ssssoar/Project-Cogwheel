using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_HatManager : MonoBehaviour{
    public static SCR_HatManager instance;
    private void Awake(){
        if (SCR_HatManager.instance != null) Destroy(SCR_HatManager.instance.gameObject);
        instance = this;
    }

    public bool changed = false;

    public void ToggleHat(){
        changed = true;
        switch((Hat)PlayerPrefs.GetInt("Hat")){
            case(Hat.none):
                PlayerPrefs.SetInt("Hat", (int)Hat.huaso);
            break;
            case(Hat.huaso):
                PlayerPrefs.SetInt("Hat", (int)Hat.none);
            break;
        }
    }

    public Hat GetHat(){
        return((Hat)PlayerPrefs.GetInt("Hat"));
    }

    void Update(){
        changed = false;
    }
}
