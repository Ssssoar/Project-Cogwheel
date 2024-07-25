using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SCR_PlayerMovement : MonoBehaviour{
    public static SCR_PlayerMovement instance;
    private void Awake(){
        if (SCR_PlayerMovement.instance != null) Destroy(SCR_PlayerMovement.instance);
        instance = this;
    }

    public SCR_FacingManager facingScript;
    public SCR_WorldPositioner positionScript;
    public float bumpTime; //EXTREMELY IMPORTANT THIS MUST BE LESS THAN THE TIME BETWEEN COMMANDS IN THE SCHEDULER

    float bumpTimer = -1f;
    float cornerTimer = -1f;
    float turnTimer = -1f;
    float lockTimer = -1f;
    Vector2 actualPos;
    Vector2 nextPos;
    Vector2 cornerPos;
    Vector2Int startPos;
    Vector2Int REALPOS = new Vector2Int(-1,-1);
    Vector2Int nullish = new Vector2Int(-1,-1);

    void Update(){
        if (bumpTimer >= 0f){
            bumpTimer -= Time.deltaTime;
            if (bumpTimer < 0f){
                SCR_LevelGenerator.instance.SwitchObjects(startPos, (REALPOS == nullish)? actualPos : REALPOS);
                REALPOS = nullish;
                SCR_Scheduler.instance.blocked = false;
            }
        }
        if (cornerTimer >= 0f){
            cornerTimer -= Time.deltaTime;
            if (cornerTimer < 0f){
                MoveInto(nextPos , cornerPos);
            }
        }
        if(turnTimer >= 0f){
            turnTimer -= Time.deltaTime;
            if (turnTimer < 0f){
                lockTimer = bumpTime * 5;
                SCR_LevelGenerator.instance.Rotate();
            }
        }
        if(lockTimer >= 0f){
            lockTimer -= Time.deltaTime;
            if (lockTimer < 0f){
                SCR_Scheduler.instance.blocked = false;
            }
        }
    }

    public void TryMovement(Command movement){
        startPos = positionScript.IntDesiredWorldPos();
        Vector2Int gridPos = startPos;
        gridPos += SCR_LevelGenerator.instance.DirectionToVect(movement);
        MoveInto(gridPos , positionScript.IntDesiredWorldPos());
    }

    void MoveInto(Vector2 gridPos , Vector2 currentPos){
        Vector2Int intGridPos = new Vector2Int((int)gridPos.x , (int)gridPos.y);
        Vector2Int intCurrentPos = new Vector2Int((int)currentPos.x , (int)currentPos.y);
        MoveInto(intGridPos , intCurrentPos);
    }

    void MoveInto(Vector2Int gridPos , Vector2Int currentPos){
        SCR_WorldPositioner bumped = SCR_LevelGenerator.instance.GetElementAtCoord(gridPos);
        switch(bumped.tag){
            case("Wall"): //it stops movement. It bumps against the wall
                { //LMAO SCOPE
                    SCR_Scheduler.instance.blocked = true;
                    Vector2 bumpPos = (currentPos + (bumped.desiredWorldPos - currentPos)*0.35f);
                    actualPos = positionScript.desiredWorldPos;
                    positionScript.desiredWorldPos = bumpPos;
                    bumpTimer = bumpTime;
                    SCR_NotePicker.instance.PlayNote(Pitch.low);
                }
            break;
            case("Corner"): //query the corner for where to move
                { //LMAO SCOPE
                    if (REALPOS == nullish) REALPOS = currentPos;
                    SCR_Scheduler.instance.blocked = true;
                    nextPos = bumped.GetComponent<SCR_Corner>().ReturnPosition(currentPos);
                    float bumpDist = (nextPos == currentPos)? 0.35f : 0.85f;
                    Vector2 bumpPos = currentPos + (bumped.desiredWorldPos - currentPos)*bumpDist;
                    cornerPos = new Vector2((float)gridPos.x , (float)gridPos.y);
                    positionScript.desiredWorldPos = bumpPos;
                    cornerTimer = bumpTime;
                    SCR_NotePicker.instance.PlayNote(Pitch.low);
                }
            break;
            case("Space"): //allow movement
                if (SCR_LevelGenerator.instance.SeekButton(gridPos)){
                    SCR_NotePicker.instance.PlayChord();
                    turnTimer = bumpTime;
                    SCR_Scheduler.instance.blocked = true;
                }else{
                    SCR_NotePicker.instance.PlayNote(Pitch.high);
                    SCR_Scheduler.instance.blocked = false;
                }
                SCR_LevelGenerator.instance.SwitchObjects(startPos , gridPos);
                REALPOS = nullish;
            break;
            case("End"): //moves, but steps on top
                positionScript.desiredWorldPos = new Vector2((float)gridPos.x, (float)gridPos.y);
                SCR_SequenceReferenceHolder.instance.nextLevelSequence.Play();
            break;
            case("Collectable"):
                bumped.GetComponent<SCR_WhenCollected>().Execute();
                SCR_LevelGenerator.instance.AddObject(gridPos, null);
                SCR_LevelGenerator.instance.SwitchObjects(startPos , gridPos);
                SCR_NotePicker.instance.PlayNote(Pitch.high);
                SCR_NotePicker.instance.PlayNote(Pitch.low);
                SCR_Scheduler.instance.blocked = false;
            break;
            case("Player"):
                SCR_LevelGenerator.instance.SwitchObjects(startPos, gridPos);
                SCR_Scheduler.instance.blocked = false;
            break;
        }
    }
}
