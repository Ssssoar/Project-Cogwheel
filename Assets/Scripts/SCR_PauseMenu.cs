using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PauseMenu : MonoBehaviour{
    public GameObject pauseMenu;
    public KeyCode menuKey;

    bool paused = false;
    
    void Update(){
        if(SCR_LevelGenerator.instance == null) return;
        if(Input.GetKeyDown(menuKey)){
            TogglePauseState();
        }
    }

    public void TogglePauseState(){
        paused = !paused;
        pauseMenu.SetActive(paused);
        if(SCR_MouseInputReceiver.instance != null){
            SCR_MouseInputReceiver.instance.canMove = !paused;
        }
        if(SCR_Scheduler.instance != null){
            SCR_Scheduler.instance.blocked = paused;
        }
    }
}
