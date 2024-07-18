using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SCR_LevelSpawner : MonoBehaviour{
    public static SCR_LevelSpawner instance;
    private void Awake(){
        if (SCR_LevelSpawner.instance != null) Destroy(SCR_LevelSpawner.instance.gameObject);
        instance = this;
    }

    public GameObject levelToSpawn;
    public GameObject buttonToKill;
    public PlayableDirector sequence;
    public Color bgColor;
    public float lerpFactor;

    void Update(){
        if (sequence.state == PlayState.Playing)
            SCR_CameraFocuser.instance.cameraComp.backgroundColor = Color.Lerp(SCR_CameraFocuser.instance.cameraComp.backgroundColor , bgColor , lerpFactor * Time.deltaTime);
    }

    // Update is called once per frame
    public void SpawnLevel(){
        Instantiate(levelToSpawn,Vector3.zero,Quaternion.identity);
    }

    public void KillButton(){
        Destroy(buttonToKill);
    }
}
