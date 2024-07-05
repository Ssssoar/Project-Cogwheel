using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlayerMovement : MonoBehaviour{
    public static SCR_PlayerMovement instance;
    private void Awake(){
        if (SCR_PlayerMovement.instance != null) Destroy(SCR_PlayerMovement.instance);
        instance = this;
    }

    public SCR_FacingManager facingScript;
    public SCR_WorldPositioner positionScript;
    public bool test;

    void Update(){
        if (!test) return;
        if (Input.GetKeyDown("w")){
            TryMovement(Command.up);
        }
        if (Input.GetKeyDown("a")){
            TryMovement(Command.left);
        }
        if (Input.GetKeyDown("s")){
            TryMovement(Command.down);
        }
        if (Input.GetKeyDown("d")){
            TryMovement(Command.right);
        }
    }

    public void TryMovement(Command movement){
        facingScript.ChangeFacing(movement);
        Vector2Int gridPos = positionScript.IntDesiredWorldPos();
        gridPos += SCR_LevelGenerator.instance.DirectionToVect(movement);
        SCR_WorldPositioner bumped = SCR_LevelGenerator.instance.GetElementAtCoord(gridPos);
        switch(bumped.tag){
            case("Wall"): //it stops movement
            break;
            case("Space"): //allow movement
                SCR_LevelGenerator.instance.SwitchObjects(positionScript.IntDesiredWorldPos() , gridPos);
            break;
            case("End"): //moves, but steps on top
                positionScript.desiredWorldPos = new Vector2((float)gridPos.x, (float)gridPos.y);
                SCR_MouseInputReceiver.instance.canMove = false;
            break;
            case("Collectable"):
                bumped.GetComponent<SCR_WhenCollected>().Execute();
                SCR_LevelGenerator.instance.ReplaceObject(gridPos, null);
                SCR_LevelGenerator.instance.SwitchObjects(positionScript.IntDesiredWorldPos() , gridPos);
            break;
        }
    }
}
