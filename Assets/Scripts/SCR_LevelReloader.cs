using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_LevelReloader : MonoBehaviour{
    public static SCR_LevelReloader instance;
    private void Awake(){
        if (SCR_LevelReloader.instance != null) Destroy(gameObject);
        else instance = this;
    }

    public GameObject levelToReload;

    public void Restart(){
        SCR_ClickCounter.instance.ResetCount();
        SCR_Scheduler.instance.CancelAllCommands();
        Instantiate(levelToReload);
    }
}
