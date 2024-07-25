using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_PauseMenu : MonoBehaviour{
    public GameObject endIfTheseActive;
    public GameObject pauseMenu;
    public KeyCode menuKey;

    bool paused = false;
    
    void Update(){
        if(SCR_LevelGenerator.instance == null) return;
        if(Input.GetKeyDown(menuKey)){
            if(endIfTheseActive.activeInHierarchy){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }else
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
