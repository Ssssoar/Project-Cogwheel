using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_RandomAnimTriggerSend : MonoBehaviour{
    public float minTime;
    public float maxTime;
    public string triggerName;
    public Animator animComp;


    float timer = -1f;
    void Update(){
        timer -= Time.deltaTime;
        if (timer <= 0){
            animComp.SetTrigger(triggerName);
            timer = Random.Range(minTime,maxTime);
        }
    }
}
