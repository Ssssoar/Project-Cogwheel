using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlayerMovement : MonoBehaviour{
    public static SCR_PlayerMovement instance;
    private void Awake(){
        if (SCR_PlayerMovement.instance != null) Destroy(gameObject);
        else instance = this;
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
        switch(SCR_LevelGenerator.instance.GetElementAtCoord(gridPos).tag){
            case("Wall"): //it stops movement
            break;
            case("Space"): //allow movement
                SCR_LevelGenerator.instance.SwitchObjects(positionScript.IntDesiredWorldPos() , gridPos);
            break;
        }
    }
}
