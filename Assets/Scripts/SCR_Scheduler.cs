using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_Scheduler : MonoBehaviour{
    public static SCR_Scheduler instance;
    private void Awake(){
        if (SCR_Scheduler.instance != null) Destroy(gameObject);
        else instance = this;
    }

    [Header("References")]
    public TMP_Text levelName;

    [Header("Prefabs")]
    public GameObject smokePuffUI;

    [Header("Variables")]
    public Transform[] queuePositions;
    public float lerpStrength = 20f;
    public bool blocked;
    public int rotations = 0;

    int waitBeats = 2;
    List<Command> waitingCommands = new List<Command>();
    List<GameObject> pendingArrows = new List<GameObject>();

    void Update(){
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

    public void AddRotations(int toAdd){
        rotations += toAdd;
        if (rotations < 0)
            rotations += 4;
        if (rotations >= 4)
            rotations -= 4;
    }

    public Command DoRotations(Command command){
        for(int i = 0; i < rotations; i++){
            command = RotateCommand(command);
        }
        return command;
    }

    public Command RotateCommand(Command preRotation){ //ROTATES CLOCKWISE
        if (preRotation == Command.up)
            return Command.right;
        if (preRotation == Command.right)
            return Command.down;
        if (preRotation == Command.down)
            return Command.left;
        if (preRotation == Command.left)
            return Command.up;
        return preRotation;
    }

    public void Beat(){
        if (blocked) return;
        if (waitingCommands.Count > 0){
            if (waitBeats > 0){
                waitBeats--;
                return;
            }
            Instantiate(smokePuffUI,pendingArrows[0].transform.position,Quaternion.identity,transform);
            ExecuteCommand(DoRotations(waitingCommands[0]));
            waitingCommands.RemoveAt(0);
            GameObject toDestroy = pendingArrows[0];
            pendingArrows.RemoveAt(0);
            Destroy(toDestroy);
            if (waitingCommands.Count == 0){
                waitBeats = 2;
            }
        }
    }
    
    public void ReceiveCommand(Command comm,GameObject arrow){
        waitingCommands.Add(comm);
        pendingArrows.Add(arrow);
    }

    void ExecuteCommand(Command comm){
        SCR_PlayerMovement.instance.TryMovement(comm);
    }

    [ContextMenu ("Remove All Commands")]
    public void CancelAllCommands(){
        waitingCommands.Clear();
        while(pendingArrows.Count > 0){
            Instantiate(smokePuffUI,pendingArrows[0].transform.position,Quaternion.identity,transform);
            Destroy(pendingArrows[0]);
            pendingArrows.RemoveAt(0);
        }
    }

    public void UpdateLevelName(string newName){
        levelName.text = newName;
    }
}
