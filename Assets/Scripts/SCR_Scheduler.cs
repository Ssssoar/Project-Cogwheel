using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Scheduler : MonoBehaviour{
    public static SCR_Scheduler instance;
    private void Awake(){
        if (SCR_Scheduler.instance != null) Destroy(gameObject);
        else instance = this;
    }

    [Header("Variables")]
    public Transform[] queuePositions;
    public float stepTime = 0.5f;
    public float lerpStrength = 20f;

    float timer;
    List<Command> waitingCommands = new List<Command>();
    List<GameObject> pendingArrows = new List<GameObject>();

    void Start(){
        timer = stepTime;
    }

    void Update(){
        timer -= Time.deltaTime;
        if(timer <= 0f){
            timer += stepTime;
            if (waitingCommands.Count > 0){
                ExecuteCommand(waitingCommands[0]);
                waitingCommands.RemoveAt(0);
                GameObject toDestroy = pendingArrows[0];
                pendingArrows.RemoveAt(0);
                Destroy(toDestroy);
            }
        }
        for(int i = 0; i < pendingArrows.Count; i++){
            Transform moveTo;
            if (i < queuePositions.Length)
                moveTo = queuePositions[i];
            else
                moveTo = queuePositions[queuePositions.Length-1];
            //Debug.Log(pendingArrows[i]);
            pendingArrows[i].transform.position = Vector3.Lerp(pendingArrows[i].transform.position , moveTo.position , lerpStrength * Time.deltaTime);
        }
    }
    
    public void ReceiveCommand(Command comm,GameObject arrow){
        waitingCommands.Add(comm);
        pendingArrows.Add(arrow);
    }

    void ExecuteCommand(Command comm){
        //Debug.Log(comm);
    }
}
